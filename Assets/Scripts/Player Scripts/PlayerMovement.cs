using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private CharacterController characterController;
  private Vector3 moveDirection;
  private float verticalVelocity;
  private float gravity = 9.8f;
  public float movementSpeed = 3f;
  public float jumpForce = 4f;
  public float rotateDegreesPerSecond = 90f;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();
  } // Awake

  void Update()
  {
    MoveThePlayer();
    PlayerRotate();
  } // Update

  void MoveThePlayer()
  {
    moveDirection = new Vector3(0f, 0f, Input.GetAxis(Axis.VERTICAL));
    moveDirection *= movementSpeed * Time.deltaTime;
    moveDirection = transform.TransformDirection(moveDirection);
    ApplyGravity();
    characterController.Move(moveDirection);
  } // Move player

  void ApplyGravity()
  {
    verticalVelocity -= gravity * Time.deltaTime;
    PlayerJump();
    moveDirection.y = verticalVelocity * Time.deltaTime;
  } // Apply gravity

  void PlayerJump()
  {
    if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
    {
      verticalVelocity = jumpForce;
    }
  } // Player jump

  void PlayerRotate()
  {
    Vector3 rotationDirection = Vector3.zero;
    if (Input.GetAxis(Axis.HORIZONTAL) < 0)
    {
      rotationDirection = transform.TransformDirection(Vector3.left);
    }
    if (Input.GetAxis(Axis.HORIZONTAL) > 0)
    {
      rotationDirection = transform.TransformDirection(Vector3.right);
    }
    if (rotationDirection != Vector3.zero)
    {
      transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        Quaternion.LookRotation(rotationDirection),
        rotateDegreesPerSecond * Time.deltaTime
      );
    }
  } // Player rotate
} // Class