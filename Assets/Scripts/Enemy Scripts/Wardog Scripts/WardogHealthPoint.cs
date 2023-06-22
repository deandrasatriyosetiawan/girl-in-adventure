using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WardogHealthPoint : EnemyHealthPoint
{
  public static WardogHealthPoint Instance { get; set; }
  private WardogAnimation wardogAnimation;
  private Animator animator;
  private WardogController wardogController;
  public GameObject wardogStatus;
  [HideInInspector]
  public float wardogMaxHealth = 100f;
  [HideInInspector]
  public float wardogCurrentHealth;
  [HideInInspector]
  public bool isWardogHealthPointActive = false;
  private bool isWardogDead = false;

  void Awake()
  {
    wardogAnimation = GetComponent<WardogAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  private void Start()
  {
    wardogCurrentHealth = wardogMaxHealth;
  } // Start

  public override void ApplyDamage(float damage)
  {
    wardogCurrentHealth -= damage;
    if (wardogCurrentHealth > 0)
    {
      wardogAnimation.Attacked();
    }
    else
    {
      if (!isWardogDead)
      {
        animator.CrossFade(EnemyAnimationTag.DIE, 0f);
        GetComponent<WardogController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        isWardogDead = true;
        isWardogHealthPointActive = false;
        DeactivateWardogStatus();
        StartCoroutine(DisableAnimator());
      }
    }
  } // Apply the damage

  IEnumerator DisableAnimator()
  {
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    animator.enabled = false;
  } // Disable the animator

  void DeactivateWardogStatus()
  {
    if (wardogStatus.activeInHierarchy)
    {
      wardogStatus.SetActive(false);
    }
  } // Deactivate the wardog status
} // Class