using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveInformation 
{
    private static SaveInformation instance;
    private SaveInformation()
    { }

    public static SaveInformation getInstance()
    {
        if(instance == null)
            instance = new SaveInformation();
        return instance;
    }
    public int PlayerHealth { get; set; }
    public int SpiderHealth { get; set; } = 300;

    public float[] playerPosition = new float[3];

    public bool FrogsKilled { get; set; } = GameObject.FindGameObjectsWithTag("Frog").Length == 0;

    public int dialogNumber = 0;

    public override string ToString()
    {
        return $"PlayerHealth: {PlayerHealth}; SpiderHealth: {SpiderHealth}; FrogsKilled: {FrogsKilled}; DialogNumber: {dialogNumber}";
    }

    public void SaveInfoToFile()
    {  
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saveFile.bin", FileMode.Create))
            {
                formatter.Serialize(fs, instance);
            }
            Debug.Log(instance.ToString());
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
                instance = (SaveInformation)formatter.Deserialize(fs);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("exception on deserializing");
        }
    }
}
