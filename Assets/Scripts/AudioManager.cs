using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

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

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.Source.Stop();
    }
}
