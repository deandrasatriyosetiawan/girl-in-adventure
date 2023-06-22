using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject punchPoint;
  public GameObject kickPoint;

  void ActivatePunchPoint()
  {
    punchPoint.SetActive(true);
  } // Activate the punch point

  void DeactivatePunchPoint()
  {
    if (punchPoint.activeInHierarchy)
    {
      punchPoint.SetActive(false);
    }
  } // Deactivate the punch point

  void ActivateKickPoint()
  {
    kickPoint.SetActive(true);
  } // Activate the kick point

  void DeactivateKickPoint()
  {
    if (kickPoint.activeInHierarchy)
    {
      kickPoint.SetActive(false);
    }
  } // Deactivate the kick point
} // Class