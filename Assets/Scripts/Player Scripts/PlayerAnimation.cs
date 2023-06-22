using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  private Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
  } // Awake

  void Update()
  {
    Run();
    Jump();
    Punch();
    Kick();
  } // Update

  public void Run()
  {
    if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)))
    {
      animator.SetBool(PlayerAnimationTag.RUN, true);
    }
    else
    {
      animator.SetBool(PlayerAnimationTag.RUN, false);
    }
  } // Run

  public void Jump()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      animator.SetBool(PlayerAnimationTag.JUMP, true);
    }
    else
    {
      animator.SetBool(PlayerAnimationTag.JUMP, false);
    }
  } // Jump

  public void Punch()
  {
    if (Input.GetKeyDown(KeyCode.Z))
    {
      animator.SetBool(PlayerAnimationTag.PUNCH, true);
    }
    else
    {
      animator.SetBool(PlayerAnimationTag.PUNCH, false);
    }
  } // Punch

  public void Kick()
  {
    if (Input.GetKeyDown(KeyCode.X))
    {
      animator.SetBool(PlayerAnimationTag.KICK, true);
    }
    else
    {
      animator.SetBool(PlayerAnimationTag.KICK, false);
    }
  } // Kick

  public void Attacked()
  {
    animator.SetTrigger(PlayerAnimationTag.ATTACKED);
  } // Attacked
} // Class