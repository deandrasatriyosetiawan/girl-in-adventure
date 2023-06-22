using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
  private Transform playerTransform;
  [SerializeField]
  private Vector3 offsetPosition;

  void Awake()
  {
    playerTransform = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).transform;
  } // Awake

  void LateUpdate()
  {
    FollowPlayer();
  } // Late update

  void FollowPlayer()
  {
    transform.position = playerTransform.TransformPoint(offsetPosition);
    transform.rotation = playerTransform.rotation;
  } // Follow the player
} // Class
