using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Level-01-Cave");
    }

    public void NewGame()
    {
        try
        {
            if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
            {
                File.Delete(Application.persistentDataPath + @"\saveFile.bin");
            }
        }
        catch
        {
            Debug.Log("exeption on deleting save file");
        }
        SceneManager.LoadScene("LoadScreen 0");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
