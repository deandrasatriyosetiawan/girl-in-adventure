using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SolgirlHealthPoint : EnemyHealthPoint
{
  public static SolgirlHealthPoint Instance { get; set; }
  private SolgirlAnimation solgirlAnimation;
  private Animator animator;
  private SolgirlController solgirlController;
  public GameObject solgirlStatus;
  [HideInInspector]
  public float solgirlMaxHealth = 100f;
  [HideInInspector]
  public float solgirlCurrentHealth;
  [HideInInspector]
  public bool isSolgirlHealthPointActive = false;
  private bool isSolgirlDead = false;

  void Awake()
  {
    solgirlAnimation = GetComponent<SolgirlAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  private void Start()
  {
    solgirlCurrentHealth = solgirlMaxHealth;
  } // Start

  public override void ApplyDamage(float damage)
  {
    solgirlCurrentHealth -= damage;
    if (solgirlCurrentHealth > 0)
    {
      solgirlAnimation.Attacked();
    }
    else
    {
      if (!isSolgirlDead)
      {
        animator.CrossFade(EnemyAnimationTag.DIE, 0f);
        GetComponent<SolgirlController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        isSolgirlDead = true;
        isSolgirlHealthPointActive = false;
        DeactivateSolgirlStatus();
        StartCoroutine(DisableAnimator());
      }
    }
  } // Apply the damage

  IEnumerator DisableAnimator()
  {
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    animator.enabled = false;
  } // Disable the animator

  void DeactivateSolgirlStatus()
  {
    if (solgirlStatus.activeInHierarchy)
    {
      solgirlStatus.SetActive(false);
    }
  } // Deactivate the solgirl status
} // Class