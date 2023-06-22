using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardogHealthBar : MonoBehaviour
{
  private Slider slider;
  public Text healthCounter;
  private WardogHealthPoint wardogHealthPoint;
  private float currentHealth, maxHealth, fillHealthValue;

  private void Awake()
  {
    slider = GetComponent<Slider>();
    wardogHealthPoint = null;
  } // Awake

  private void Start()
  {
    UpdateWardogHealthPoint();
  } // Start

  void Update()
  {
    if (wardogHealthPoint == null || wardogHealthPoint.wardogCurrentHealth <= 0)
    {
      UpdateWardogHealthPoint();
      if (wardogHealthPoint == null)
      {
        SetHealthBar(0f, 0f);
        return;
      }
    }
    maxHealth = wardogHealthPoint.wardogMaxHealth;
    if (wardogHealthPoint.wardogCurrentHealth >= 0)
    {
      currentHealth = wardogHealthPoint.wardogCurrentHealth;
    }
    else
    {
      currentHealth = 0f;
    }
    fillHealthValue = currentHealth / maxHealth;
    SetHealthBar(currentHealth, fillHealthValue);
  } // Update

  private void UpdateWardogHealthPoint()
  {
    WardogHealthPoint[] wardogs = FindObjectsOfType<WardogHealthPoint>();
    if (wardogs.Length > 0)
    {
      foreach (WardogHealthPoint wardog in wardogs)
      {
        if (wardog.isWardogHealthPointActive)
        {
          wardogHealthPoint = wardog;
        }
      }
    }
    else
    {
      wardogHealthPoint = null;
    }
  } // Update wardog health point

  private void SetHealthBar(float currentHealth, float fillValue)
  {
    slider.value = fillValue;
    healthCounter.text = currentHealth.ToString("F0") + "%";
  } // Set health bar
} // Class