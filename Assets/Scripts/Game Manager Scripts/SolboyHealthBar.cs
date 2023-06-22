using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolboyHealthBar : MonoBehaviour
{
  private Slider slider;
  public Text healthCounter;
  private SolboyHealthPoint solboyHealthPoint;
  private float currentHealth, maxHealth, fillHealthValue;

  private void Awake()
  {
    slider = GetComponent<Slider>();
    solboyHealthPoint = null;
  } // Awake

  private void Start()
  {
    UpdateSolboyHealthPoint();
  } // Start

  void Update()
  {
    if (solboyHealthPoint == null || solboyHealthPoint.solboyCurrentHealth <= 0)
    {
      UpdateSolboyHealthPoint();
      if (solboyHealthPoint == null)
      {
        SetHealthBar(0f, 0f);
        return;
      }
    }
    maxHealth = solboyHealthPoint.solboyMaxHealth;
    if (solboyHealthPoint.solboyCurrentHealth >= 0)
    {
      currentHealth = solboyHealthPoint.solboyCurrentHealth;
    }
    else
    {
      currentHealth = 0f;
    }
    fillHealthValue = currentHealth / maxHealth;
    SetHealthBar(currentHealth, fillHealthValue);
  } // Update

  private void UpdateSolboyHealthPoint()
  {
    SolboyHealthPoint[] solboys = FindObjectsOfType<SolboyHealthPoint>();
    if (solboys.Length > 0)
    {
      foreach (SolboyHealthPoint solboy in solboys)
      {
        if (solboy.isSolboyHealthPointActive)
        {
          solboyHealthPoint = solboy;
        }
      }
    }
    else
    {
      solboyHealthPoint = null;
    }
  } // Update solboy health point

  private void SetHealthBar(float currentHealth, float fillValue)
  {
    slider.value = fillValue;
    healthCounter.text = currentHealth.ToString("F0") + "%";
  } // Set health bar
} // Class