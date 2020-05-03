using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Continue();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
