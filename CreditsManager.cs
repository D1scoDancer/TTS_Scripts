using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsManager : MonoBehaviour
{
    public float time;
    public GameObject button;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
