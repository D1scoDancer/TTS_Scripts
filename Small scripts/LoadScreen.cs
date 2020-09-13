using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Сlass that calls the loading screen
/// </summary>
public class LoadScreen : MonoBehaviour
{
    public float time;

    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Waiting before loading the game
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level-01-Cave");
    }
}