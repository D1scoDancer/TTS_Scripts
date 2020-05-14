using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс загружающий экран загрузки
/// </summary>
public class LoadScreen : MonoBehaviour
{
    public float time;

    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Ожидание перед загрузкой игры
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level-01-Cave");
    }
}