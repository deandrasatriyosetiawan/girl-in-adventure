using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SolboyController : MonoBehaviour
{
  private SolboyAnimation solboyAnimation;
  private SolboyHealthPoint solboyHealthPoint;
  private NavMeshAgent navMeshAgent;
  private Transform playerTransform;
  public GameObject attackPoint;
  private PlayerHealthPoint playerHealthPoint;
  public GameObject solboyStatus;
  private RaycastHit hit;
  private float playerHP;
  private float attackTimer;
  private float distanceFromPlayer;
  private float waitBeforeAttackTime = 2f;
  private bool isChasingPlayer = false;
  private bool isAttackingPlayer = false;
  public float movementSpeed = 1.5f;
  public float attackRange = 0.8f;
  public float chaseRange = 6f;

  void Awake()
  {
    solboyAnimation = GetComponent<SolboyAnimation>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    playerTransform = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).transform;
    playerHealthPoint = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).GetComponent<PlayerHealthPoint>();
    solboyHealthPoint = GetComponent<SolboyHealthPoint>();
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
        ActivateSolboyStatus();
        solboyHealthPoint.isSolboyHealthPointActive = true;
        isChasingPlayer = true;
      }
    }
    else
    {
      navMeshAgent.velocity = Vector3.zero;
      solboyAnimation.Walk(false);
      isChasingPlayer = false;
      isAttackingPlayer = false;
      navMeshAgent.isStopped = true;
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
      solboyAnimation.Walk(true);
    }
    else
    {
      solboyAnimation.Walk(false);
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
    solboyAnimation.Walk(false);
    attackTimer += Time.deltaTime;
    if (attackTimer > waitBeforeAttackTime)
    {
      solboyAnimation.Attack();
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

  private void AdjustHeightToTerrain()
  {
    if (Physics.Raycast(transform.position, -Vector3.up, out hit))
    {
      Vector3 position = transform.position;
      position.y = hit.point.y;
      transform.position = position;
    }
  } // Adjust height to terrain

  void ActivateSolboyStatus()
  {
    solboyStatus.SetActive(true);
  } // Activate the solboy status
} // Class