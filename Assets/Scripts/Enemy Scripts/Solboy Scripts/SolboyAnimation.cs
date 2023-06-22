using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolboyAnimation : MonoBehaviour
{
  private Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
  } // Awake

  public void Walk(bool walk)
  {
    animator.SetBool(EnemyAnimationTag.WALK, walk);
  } // Walk

  public void Attack()
  {
    animator.SetTrigger(EnemyAnimationTag.ATTACK);
  } // Attack

  public void Attacked()
  {
    animator.SetTrigger(EnemyAnimationTag.ATTACKED);
  } // Attacked
} // Class