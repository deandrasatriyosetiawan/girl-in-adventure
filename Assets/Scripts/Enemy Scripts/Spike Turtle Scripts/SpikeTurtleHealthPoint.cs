using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpikeTurtleHealthPoint : EnemyHealthPoint
{
  public float spikeTurtleHealth = 50f;
  private SpikeTurtleAnimation spikeTurtleAnimation;
  private Animator animator;
  private bool isSpikeTurtleDead = false;

  void Awake()
  {
    spikeTurtleAnimation = GetComponent<SpikeTurtleAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  public override void ApplyDamage(float damage)
  {
    spikeTurtleHealth -= damage;
    if (spikeTurtleHealth > 0)
    {
      spikeTurtleAnimation.Attacked();
    }
    else
    {
      if (!isSpikeTurtleDead)
      {
        animator.CrossFade(EnemyAnimationTag.DIE, 0f);
        GetComponent<SpikeTurtleController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        isSpikeTurtleDead = true;
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
