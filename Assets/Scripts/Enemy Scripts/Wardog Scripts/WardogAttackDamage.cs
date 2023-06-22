using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardogAttackDamage : MonoBehaviour
{
  public LayerMask layerMask;
  public float radius = 0.5f;
  private float damage = 20f;

  void Update()
  {
    Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
    if (hits.Length > 0)
    {
      hits[0].GetComponent<PlayerHealthPoint>().ApplyDamage(damage);
      gameObject.SetActive(false);
    }
  } // Update
} // Class
