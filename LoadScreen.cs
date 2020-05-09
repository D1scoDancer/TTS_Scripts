using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    public float time;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level-01-Cave");
    }
}
