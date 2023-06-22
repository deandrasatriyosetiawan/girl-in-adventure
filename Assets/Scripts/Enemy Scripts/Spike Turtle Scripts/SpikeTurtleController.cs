using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpikeTurtleController : MonoBehaviour
{
  private SpikeTurtleAnimation spikeTurtleAnimation;
  private NavMeshAgent navMeshAgent;
  private Transform playerTransform;
  public GameObject attackPoint;
  private PlayerHealthPoint playerHealthPoint;
  private Vector3 randomDestination;
  private LayerMask layerMask;
  private RaycastHit hit;
  private float playerHP;
  private float attackTimer;
  private float distanceFromPlayer;
  private float maxRandomMoveDistance = 10f;
  private float waitBeforeAttackTime = 2f;
  private bool isChasingPlayer = false;
  private bool isAttackingPlayer = false;
  private bool isMovingRandomly = false;
  private float randomMovementTimer;
  public float randomMovementTime = 3f;
  public float movementSpeed = 1.5f;
  public float attackRange = 0.8f;
  public float chaseRange = 7f;
  private float collisionRadius = 0.6f;

  void Awake()
  {
    spikeTurtleAnimation = GetComponent<SpikeTurtleAnimation>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    playerTransform = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).transform;
    playerHealthPoint = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).GetComponent<PlayerHealthPoint>();
    layerMask = LayerMask.GetMask(CharacterTag.SLIME, CharacterTag.SPIKE_TURTLE);
  } // Awake

  void Start()
  {
    attackTimer = waitBeforeAttackTime;
  } // Start

  void Update()
  {
    distanceFromPlayer = GetDistanceFromPlayer();
    playerHP = playerHealthPoint.playerCurrentHealth;
    if (playerHP > 0f)
    {
      if (distanceFromPlayer <= chaseRange)
      {
        isChasingPlayer = true;
      }
    }
    else
    {
      RandomMovement();
      AdjustHeightToTerrain();
      CheckCollision();
      ReachedDestination();
      return;
    }
    if (isChasingPlayer)
    {
      ChasePlayer();
      AdjustHeightToTerrain();
    }
    if (isAttackingPlayer)
    {
      AttackPlayer();
    }
  } // Update

  void ChasePlayer()
  {
    navMeshAgent.SetDestination(playerTransform.position);
    navMeshAgent.speed = movementSpeed;
    if (navMeshAgent.velocity.sqrMagnitude != 0)
    {
      spikeTurtleAnimation.Walk(true);
    }
    else
    {
      spikeTurtleAnimation.Walk(false);
    }
    distanceFromPlayer = GetDistanceFromPlayer();
    if (distanceFromPlayer <= attackRange)
    {
      isChasingPlayer = false;
      isAttackingPlayer = true;
    }
  } // Chase the player

  void AttackPlayer()
  {
    navMeshAgent.velocity = Vector3.zero;
    navMeshAgent.isStopped = true;
    spikeTurtleAnimation.Walk(false);
    attackTimer += Time.deltaTime;
    if (attackTimer > waitBeforeAttackTime)
    {
      spikeTurtleAnimation.Attack();
      attackTimer = 0f;
    }
    distanceFromPlayer = GetDistanceFromPlayer();
    if (distanceFromPlayer > attackRange)
    {
      navMeshAgent.isStopped = false;
      isAttackingPlayer = false;
      isChasingPlayer = true;
    }
  } // Attack the player

  private float GetDistanceFromPlayer()
  {
    float distance = Vector3.Distance(transform.position, playerTransform.position);
    return distance;
  } // Get distance from player

  void ActivateAttackPoint()
  {
    attackPoint.SetActive(true);
  } // Activate the attack point

  void DeactivateAttackPoint()
  {
    if (attackPoint.activeInHierarchy)
    {
      attackPoint.SetActive(false);
    }
  } // Deactivate the attack point

  private void RandomMovement()
  {
    isChasingPlayer = false;
    isAttackingPlayer = false;
    navMeshAgent.isStopped = false;
    if (!isMovingRandomly)
    {
      isMovingRandomly = true;
      randomMovementTimer = randomMovementTime;
      Vector3 randomDirection = Random.insideUnitSphere * movementSpeed;
      randomDirection += transform.position;
      NavMeshHit hit;
      NavMesh.SamplePosition(randomDirection, out hit, maxRandomMoveDistance, 1);
      randomDestination = hit.position;
      navMeshAgent.SetDestination(randomDestination);
      navMeshAgent.speed = movementSpeed;
      spikeTurtleAnimation.Walk(true);
    }
  } // Random movement

  private void ReachedDestination()
  {
    if (isMovingRandomly && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
    {
      spikeTurtleAnimation.Walk(false);
      randomMovementTimer -= Time.deltaTime;
      if (randomMovementTimer <= 0f)
      {
        isMovingRandomly = false;
      }
    }
  } // Reached the destination

  void CheckCollision()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);
    foreach (Collider collider in colliders)
    {
      if (collider.gameObject.layer == layerMask)
      {
        navMeshAgent.ResetPath();
        spikeTurtleAnimation.Walk(false);
        Vector3 avoidDirection = transform.position - collider.transform.position;
        navMeshAgent.Move(avoidDirection.normalized * Time.deltaTime * movementSpeed);
      }
    }
  } // Check collision

  private void AdjustHeightToTerrain()
  {
    if (Physics.Raycast(transform.position, -Vector3.up, out hit))
    {
      Vector3 position = transform.position;
      position.y = hit.point.y;
      transform.position = position;
    }
  } // Adjust height to terrain
} // Class