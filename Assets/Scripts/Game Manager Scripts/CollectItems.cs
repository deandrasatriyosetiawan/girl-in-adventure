using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
  private PlayerHealthPoint playerHealthPoint;
  private int appleLayerMask = 11, pearLayerMask = 12;
  private float healthPointFromApple = 7f, healthPointFromPear = 10f;
  private bool isCollisionWithPlayer = false;

  void Awake()
  {
    playerHealthPoint = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).GetComponent<PlayerHealthPoint>();
  } // Awake

  void Update()
  {
    if (isCollisionWithPlayer)
    {
      if (gameObject.layer == appleLayerMask)
      {
        if (playerHealthPoint.playerCurrentHealth + healthPointFromApple <= playerHealthPoint.playerMaxHealth)
        {
          playerHealthPoint.playerCurrentHealth += healthPointFromApple;
        }
        else
        {
          playerHealthPoint.playerCurrentHealth = playerHealthPoint.playerMaxHealth;
        }
      }
      else if (gameObject.layer == pearLayerMask)
      {
        if (playerHealthPoint.playerCurrentHealth + healthPointFromPear <= playerHealthPoint.playerMaxHealth)
        {
          playerHealthPoint.playerCurrentHealth += healthPointFromPear;
        }
        else
        {
          playerHealthPoint.playerCurrentHealth = playerHealthPoint.playerMaxHealth;
        }
      }
      Destroy(gameObject);
    }
  } // Update

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.CompareTag(CharacterTag.PLAYER))
    {
      isCollisionWithPlayer = true;
    }
  } // On trigger enter

  private void OnTriggerExit(Collider collider)
  {
    if (collider.CompareTag(CharacterTag.PLAYER))
    {
      isCollisionWithPlayer = false;
    }
  } // On trigger exit
} // Class