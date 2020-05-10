using System.IO;
using UnityEngine;

public class FrogsActivator : MonoBehaviour
{
    SaveManager saveManager;
    private void Start()
    {
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveManager = FindObjectOfType<SaveManager>();
            if(saveManager.saveInfo.FrogsKilled || saveManager.saveInfo.screens[0])
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateFrogs();
    }

    private void ActivateFrogs()
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
