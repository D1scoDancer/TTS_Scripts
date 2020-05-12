using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс отвечающий за работу главноего меню игры
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Кнопка продолжить игру
    /// </summary>
    public void Continue()
    {
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            SceneManager.LoadScene("Level-01-Cave");
        }
    }

    /// <summary>
    /// Кнопка начать новую игру
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
            Debug.Log("exeption on deleting save file");
        }
        SceneManager.LoadScene("LoadScreen 0");
    }

    /// <summary>
    /// Кнопка выйти из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}