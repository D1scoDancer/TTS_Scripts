﻿using System;
using UnityEngine;
/// <summary>
/// Класс отвечающий за воспроизведение всех звуков игры
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Масссив всех мелодий игры
    /// </summary>
    public Sound[] sounds;

    /// <summary>
    /// Экземпляр класса AudioManager (паттерн Одиночка)
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// Объект сохраняемой информации
    /// </summary>
    SaveManager saveManager;

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
    /// Воспроизвести мелодию
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
    /// Остановить мелодию
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