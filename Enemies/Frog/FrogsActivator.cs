using System.IO;
using UnityEngine;

/// <summary>
/// Класс активирующий лягушек
/// </summary>
public class FrogsActivator : MonoBehaviour
{
    SaveManager saveManager;
    void Start()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateFrogs();
    }

    /// <summary>
    /// Активация лягушек
    /// </summary>
    void ActivateFrogs()
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