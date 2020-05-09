using System.IO;
using UnityEngine;

public class FrogsActivator : MonoBehaviour
{
    SaveInformation saveInfo;
    private void Start()
    {
        saveInfo = SaveInformation.getInstance();
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            SaveInformation.ReadInfoFromFile();
            saveInfo = SaveInformation.getInstance();
            if(saveInfo.FrogsKilled)
            {
                Destroy(gameObject);
            }
            else
            {
                foreach(Transform frog in transform)
                {
                    if(frog.name.Contains("Frog"))
                    {
                        frog.GetComponent<FrogController>().enabled = true;
                    }
                }
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(Transform frog in transform)
        {
            if(frog.name.Contains("Frog"))
            {
                frog.GetComponent<FrogController>().enabled = true;
            }
        }
    }
}
