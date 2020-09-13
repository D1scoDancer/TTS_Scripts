using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class responsible for the operation of the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Continue game button
    /// </summary>
    public void Continue()
    {
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            SceneManager.LoadScene("Level-01-Cave");
        }
    }

    /// <summary>
    /// Start a new game button
    /// </summary>
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
            Debug.Log("exception on deleting save file");
        }
        SceneManager.LoadScene("LoadScreen 0");
    }

    /// <summary>
    /// Quit game button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}