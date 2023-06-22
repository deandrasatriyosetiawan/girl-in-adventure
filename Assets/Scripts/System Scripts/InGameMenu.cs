using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
  public void CloseInGameMenu()
  {
    MenuManager.Instance.menuCanvas.SetActive(false);
    MenuManager.Instance.isMenuOpen = false;
    Time.timeScale = 1f;
  } // Close in game menu

  public void BackToMainMenu()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneTag.MAIN_MENU);
  } // Back to main menu
} // Class