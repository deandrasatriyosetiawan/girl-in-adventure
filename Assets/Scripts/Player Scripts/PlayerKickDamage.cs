using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickDamage : MonoBehaviour
{
  public LayerMask layerMask;
  public float radius = 0.8f;
  public float damage = 70f;

  void Update()
  {
    Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
    if (hits.Length > 0)
    {
      EnemyHealthPoint enemyHealthPoint = hits[0].GetComponent<EnemyHealthPoint>();
      if (enemyHealthPoint != null)
      {
        enemyHealthPoint.ApplyDamage(damage);
      }
      gameObject.SetActive(false);
    }
  } // Update
} // Class
