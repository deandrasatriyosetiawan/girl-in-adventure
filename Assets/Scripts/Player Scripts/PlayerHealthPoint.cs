using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPoint : MonoBehaviour
{
  public static PlayerHealthPoint Instance { get; set; }
  private PlayerAnimation playerAnimation;
  private Animator animator;
  [HideInInspector]
  public float playerMaxHealth = 100f;
  [HideInInspector]
  public float playerCurrentHealth;
  private bool isPlayerDead = false;

  private void Awake()
  {
    playerAnimation = GetComponent<PlayerAnimation>();
    animator = GetComponent<Animator>();
  } // Awake

  private void Start()
  {
    playerCurrentHealth = playerMaxHealth;
  } // Start

  public void ApplyDamage(float damage)
  {
    playerCurrentHealth -= damage;
    if (playerCurrentHealth > 0)
    {
      playerAnimation.Attacked();
    }
    else
    {
      if (!isPlayerDead)
      {
        animator.CrossFade(PlayerAnimationTag.DIE, 0f);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponentInChildren<PlayerPunchDamage>().enabled = false;
        GetComponentInChildren<PlayerKickDamage>().enabled = false;
        isPlayerDead = true;
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
