using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    private string _currentMusic;

    public static AudioManager Instance;

    void Awake()
    {
        #region Singleton
            if(Instance == null)
            {
                Instance = this;
            } 
            else
            {
                Destroy(gameObject);
                return;
            }
        #endregion

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if(s == null) 
        {
            Debug.LogWarning("Sound " + name + " not found !");
            return;
        }
        s.Source.Play();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found !");
            return;
        }
        s.Source.Play();
        _currentMusic = name;
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.Source.Pause();
    }

    public void Resume(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.Source.UnPause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.Source.Stop();
        _currentMusic = null;
    }

    public bool isPlaying(string name)
    {
        return name == _currentMusic;
    }
}
