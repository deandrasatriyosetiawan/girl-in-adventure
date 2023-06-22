using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SolboyHealthPoint : EnemyHealthPoint
{
  public static SolboyHealthPoint Instance { get; set; }
  private SolboyAnimation solboyAnimation;
  private Animator animator;
  private SolboyController solboyController;
  public GameObject solboyStatus;
  [HideInInspector]
  public float solboyMaxHealth = 100f;
  [HideInInspector]
  public float solboyCurrentHealth;
  [HideInInspector]
  public bool isSolboyHealthPointActive = false;
  private bool isSolboyDead = false;

  void Awake()
  {
    solboyAnimation = GetComponent<SolboyAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  private void Start()
  {
    solboyCurrentHealth = solboyMaxHealth;
  } // Start

  public override void ApplyDamage(float damage)
  {
    solboyCurrentHealth -= damage;
    if (solboyCurrentHealth > 0)
    {
      solboyAnimation.Attacked();
    }
    else
    {
      if (!isSolboyDead)
      {
        animator.CrossFade(EnemyAnimationTag.DIE, 0f);
        GetComponent<SolboyController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        isSolboyDead = true;
        isSolboyHealthPointActive = false;
        DeactivateSolboyStatus();
        StartCoroutine(DisableAnimator());
      }
    }
  } // Apply the damage

  IEnumerator DisableAnimator()
  {
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    animator.enabled = false;
  } // Disable the animator

  void DeactivateSolboyStatus()
  {
    if (solboyStatus.activeInHierarchy)
    {
      solboyStatus.SetActive(false);
    }
  } // Deactivate the solboy status
} // Class