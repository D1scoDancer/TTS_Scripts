using System;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Сlass responsible for playing all the sounds of the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Array of all the sounds of the game
    /// </summary>
    public Sound[] sounds;

    /// <summary>
    /// Instance of the AudioManager class (Singleton pattern)
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// Object for save information
    /// </summary>
    SaveManager saveManager;

    public AudioMixerGroup group;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = group;
        }
    }

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();

        if(saveManager.saveInfo.dialogNumber == 0)
        {
            Play("MainTheme");
        }
        else if(saveManager.saveInfo.dialogNumber == 1)
        {
            Stop("BossBattle");
            Play("MainTheme");
        }
        else
        {
            Stop("BossBattle");
            Play("BossBattle");
        }
    }

    /// <summary>
    /// Play sound
    /// </summary>
    /// <param name="name">Имя мелодии</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    /// <summary>
    /// Stop sound
    /// </summary>
    /// <param name="name">Имя мелодии</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Stop();
    }
}