using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeHealthPoint : EnemyHealthPoint
{
  public float slimeHealth = 30f;
  private SlimeAnimation slimeAnimation;
  private Animator animator;
  private bool isSlimeDead = false;

  void Awake()
  {
    slimeAnimation = GetComponent<SlimeAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  public override void ApplyDamage(float damage)
  {
    slimeHealth -= damage;
    if (slimeHealth > 0)
    {
      slimeAnimation.Attacked();
    }
    else
    {
      if (!isSlimeDead)
      {
        animator.CrossFade(EnemyAnimationTag.DIE, 0f);
        GetComponent<SlimeController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        isSlimeDead = true;
        StartCoroutine(DisableAnimator());
      }
    }
  } // Apply the damage

  IEnumerator DisableAnimator()
  {
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    animator.enabled = false;
  } // Disable the animator
} // Class
