using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance;
  public AudioSource musicSource;
  public List<AudioSource> soundSources = new();

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
  }

  public void PlayMusic(AudioClip clip, float volume = 1, bool loop = true)
  {
    musicSource.clip = clip;
    musicSource.volume = volume;
    musicSource.loop = loop;
    musicSource.Play();
  }

  public void PlaySound(AudioClip clip, float volume = 1)
  {
    foreach (var source in soundSources)
    {
      if (!source.isPlaying)
      {
        source.clip = clip;
        source.volume = volume;
        source.Play();
        return;
      }
    }

    Debug.LogWarning("All soundSources are busy!");
  }

}
