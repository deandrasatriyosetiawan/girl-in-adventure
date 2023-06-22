using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance { get; set; }
  public List<AudioSource> backgroundMusicList;
  private int currentBackgroundMusicIndex = 0;

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

  private void Start()
  {
    backgroundMusicList[currentBackgroundMusicIndex].Play();
  } // Start

  private void Update()
  {
    if (!backgroundMusicList[currentBackgroundMusicIndex].isPlaying)
    {
      PlayNextBackgroundMusic();
    }
  } // Update

  private void PlayNextBackgroundMusic()
  {
    backgroundMusicList[currentBackgroundMusicIndex].Stop();
    currentBackgroundMusicIndex++;
    if (currentBackgroundMusicIndex >= backgroundMusicList.Count)
    {
      currentBackgroundMusicIndex = 0;
    }
    backgroundMusicList[currentBackgroundMusicIndex].Play();
  } // Play next background music
} // Class