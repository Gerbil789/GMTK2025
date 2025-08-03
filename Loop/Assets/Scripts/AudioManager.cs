using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance;
  [HideInInspector] public AudioSource musicSource;
  [HideInInspector] public List<AudioSource> soundSources = new();

  public List<AudioClip> backgroundMusic;
  private int currentMusicIndex = 0;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    musicSource = gameObject.AddComponent<AudioSource>();

    int soundSourcesCount = 5;
    for (int i = 0; i < soundSourcesCount; i++)
    {
      var source = gameObject.AddComponent<AudioSource>();
      soundSources.Add(source);
    }

    PlayMusic(backgroundMusic[currentMusicIndex]);
  }

  private void Update()
  {
    // Check if music has finished playing
    if (!musicSource.isPlaying && backgroundMusic.Count > 0)
    {
      PlayNextTrack();
    }
  }

  public void PlayMusic(AudioClip clip, float volume = 1.0f, bool loop = false)
  {
    musicSource.clip = clip;
    musicSource.volume = volume;
    musicSource.loop = loop;
    musicSource.Play();
  }

  public void PlaySound(AudioClip clip)
  {
    foreach (var source in soundSources)
    {
      if (!source.isPlaying)
      {
        source.clip = clip;
        source.Play();
        return;
      }
    }

    Debug.LogWarning("All soundSources are busy!");
  }

  private void PlayNextTrack()
  {
    currentMusicIndex = (currentMusicIndex + 1) % backgroundMusic.Count;
    PlayMusic(backgroundMusic[currentMusicIndex]);
  }

  public void SetMute(bool mute) 
  {
    musicSource.mute = mute;

    foreach (var source in soundSources)
    {
      source.mute = mute;
    }
  }

}
