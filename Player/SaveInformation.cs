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
    public int SpiderHealth { get; set; }

    public float[] playerPosition = new float[3];

    public bool FrogsKilled { get; set; }
    // public bool FrogsKilled { get; set; } = GameObject.FindGameObjectsWithTag("Frog").Length == 0;

    public int dialogNumber = 0;

    public bool[] screens = new bool[3];

    public override string ToString()
    {
        return $"PlayerHealth: {PlayerHealth}; SpiderHealth: {SpiderHealth}; FrogsKilled: {FrogsKilled}; DialogNumber: {dialogNumber}, screens: {screens[0]},{screens[1]},{screens[2]}";
    }

    public static void SaveInfoToFile(SaveInformation save)
    {
        try
        {
            Debug.Log("saving" + save.SpiderHealth);
            save.FrogsKilled = GameObject.FindGameObjectsWithTag("Frog").Length == 0;
            save.SpiderHealth = UnityEngine.Object.FindObjectOfType<SpiderFighter>().gameObject.GetComponent<Enemy>().health;
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saveFile.bin", FileMode.Create))
            {
                formatter.Serialize(fs, save);
            }
            Debug.Log(instance);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("exception on serializing");
        }
    }


    public static void ReadInfoFromFile()
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
