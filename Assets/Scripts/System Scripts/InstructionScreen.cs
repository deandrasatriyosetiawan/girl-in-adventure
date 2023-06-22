using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionScreen : MonoBehaviour
{
  public GameObject mainMenu, instructionScreen;

  public void PlayGame()
  {
    StartCoroutine(LoadGameScene());
  } // Play game

  private IEnumerator LoadGameScene()
  {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneTag.GAME_SCENE, LoadSceneMode.Single);
    while (!asyncLoad.isDone)
    {
      yield return null;
    }
    instructionScreen.SetActive(false);
    mainMenu.SetActive(true);
  } // Load game scene
} // Class