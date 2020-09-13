using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class responsible for showing the ending credits
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
    /// Waiting until the exit button is shown
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    /// <summary>
    /// Exit to the menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}