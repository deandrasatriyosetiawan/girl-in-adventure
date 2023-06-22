using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolgirlHealthBar : MonoBehaviour
{
  private Slider slider;
  public Text healthCounter;
  private SolgirlHealthPoint solgirlHealthPoint;
  private float currentHealth, maxHealth, fillHealthValue;

  private void Awake()
  {
    slider = GetComponent<Slider>();
    solgirlHealthPoint = null;
  } // Awake

  private void Start()
  {
    UpdateSolgirlHealthPoint();
  } // Start

  void Update()
  {
    if (solgirlHealthPoint == null || solgirlHealthPoint.solgirlCurrentHealth <= 0)
    {
      UpdateSolgirlHealthPoint();
      if (solgirlHealthPoint == null)
      {
        SetHealthBar(0f, 0f);
        return;
      }
    }
    maxHealth = solgirlHealthPoint.solgirlMaxHealth;
    if (solgirlHealthPoint.solgirlCurrentHealth >= 0)
    {
      currentHealth = solgirlHealthPoint.solgirlCurrentHealth;
    }
    else
    {
      currentHealth = 0f;
    }
    fillHealthValue = currentHealth / maxHealth;
    SetHealthBar(currentHealth, fillHealthValue);
  } // Update

  private void UpdateSolgirlHealthPoint()
  {
    SolgirlHealthPoint[] solgirls = FindObjectsOfType<SolgirlHealthPoint>();
    if (solgirls.Length > 0)
    {
      foreach (SolgirlHealthPoint solgirl in solgirls)
      {
        if (solgirl.isSolgirlHealthPointActive)
        {
          solgirlHealthPoint = solgirl;
        }
      }
    }
    else
    {
      solgirlHealthPoint = null;
    }
  } // Update solgirl health point

  private void SetHealthBar(float currentHealth, float fillValue)
  {
    slider.value = fillValue;
    healthCounter.text = currentHealth.ToString("F0") + "%";
  } // Set health bar
} // Class