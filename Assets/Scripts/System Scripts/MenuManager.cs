using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
  public static MenuManager Instance { get; set; }
  public GameObject menuCanvas;
  [HideInInspector]
  public bool isMenuOpen;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  } // Awake

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen)
    {
      Time.timeScale = 0f;
      menuCanvas.SetActive(true);
      isMenuOpen = true;
    }
  } // Update
} // Class