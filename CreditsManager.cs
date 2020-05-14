using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс отвечающий за показ финальных титров
/// </summary>
public class CreditsManager : MonoBehaviour
{
    public float time;
    public GameObject button;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Ожидание до показа кнопки выхода
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}