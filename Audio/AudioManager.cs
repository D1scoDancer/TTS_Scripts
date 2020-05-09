using System;
using UnityEngine;
using System.IO;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private SaveInformation saveInfo;

    private void Awake()
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

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    private void Start()
    {
        saveInfo = SaveInformation.getInstance();
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveInfo.ReadInfoFromFile();
            saveInfo = SaveInformation.getInstance();
        }

        if(saveInfo.dialogNumber == 0)
        {
            Play("MainTheme");
        }
        else if(saveInfo.dialogNumber == 1)
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
