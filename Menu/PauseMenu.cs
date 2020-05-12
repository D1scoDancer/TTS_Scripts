using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс отвечающий за работу меню паузы
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Поставить паузу
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    /// <summary>
    /// Кнопка продолжить
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /// <summary>
    /// Кнопка выйти в меню
    /// </summary>
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Кнопка выйти из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}