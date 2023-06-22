using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
  private Slider slider;
  public Text healthCounter;
  private PlayerHealthPoint playerHealthPoint;
  private float currentHealth, maxHealth, fillHealthValue;

  private void Awake()
  {
    slider = GetComponent<Slider>();
    playerHealthPoint = GameObject.FindGameObjectWithTag(CharacterTag.PLAYER).GetComponent<PlayerHealthPoint>();
  } // Awake

  private void Start()
  {
    maxHealth = playerHealthPoint.playerMaxHealth;
  } // Start

  void Update()
  {
    if (playerHealthPoint.playerCurrentHealth >= 0)
    {
      currentHealth = playerHealthPoint.playerCurrentHealth;
    }
    else
    {
      currentHealth = 0f;
    }
    fillHealthValue = currentHealth / maxHealth;
    SetHealthBar(currentHealth, fillHealthValue);
  } // Update

  private void SetHealthBar(float currentHealth, float fillValue)
  {
    slider.value = fillValue;
    healthCounter.text = currentHealth.ToString("F0") + "%";
  } // Set health bar
} // Class