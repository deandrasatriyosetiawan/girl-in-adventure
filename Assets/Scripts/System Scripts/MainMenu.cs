using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public GameObject mainMenu, instructionScreen;

  public void GoToInstructionScreen()
  {
    mainMenu.SetActive(false);
    instructionScreen.SetActive(true);
  } // Go to instruction screen

  public void ExitGame()
  {
    Debug.Log("Quitting Game");
    Application.Quit();
  } // Exit game
} // Class