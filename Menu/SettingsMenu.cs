﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Класс отвечающий за работу меню настроек
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Задать громкость
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    /// <summary>
    /// Задать качество
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Задать полноэкранный режим
    /// </summary>
    /// <param name="isFullScreen"></param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    /// <summary>
    /// Задать расрешение экрана
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}