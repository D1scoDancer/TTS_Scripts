using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [HideInInspector]
    public SaveInformation saveInfo;

    public void SaveInfoToFile()
    {
        try
        {
            saveInfo.FrogsKilled = GameObject.FindGameObjectsWithTag("Frog").Length == 0;
            saveInfo.SpiderHealth = FindObjectOfType<SpiderFighter>().gameObject.GetComponent<Enemy>().health;

            BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saveFile.bin", FileMode.Create))
            {
                formatter.Serialize(fs, saveInfo);
            }

            Debug.Log(saveInfo.ToString()); 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("exception on serializing");
        }
    }


    public void ReadInfoFromFile()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saveFile.bin", FileMode.Open))
            {
                fs.Position = 0;
                SaveInformation file = (SaveInformation)formatter.Deserialize(fs);
                this.Rewrite(file);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("exception on deserializing");
        }
    }

    private void Rewrite(SaveInformation file)
    {
        saveInfo.SpiderHealth = file.SpiderHealth;
        saveInfo.playerPosition = file.playerPosition;
        saveInfo.FrogsKilled = file.FrogsKilled;
        saveInfo.dialogNumber = file.dialogNumber;
        saveInfo.screens = file.screens;
    }

    private void Awake()
    {
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            ReadInfoFromFile();
        }
        else
        {
            Transform player = FindObjectOfType<Player>().transform;
            saveInfo.playerPosition = new float[3] { player.position.x, player.position.y, player.position.z };
            SaveInfoToFile();
        }
    }
}


