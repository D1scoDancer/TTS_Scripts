using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that stores data of the progress of the game
/// </summary>
public class SavePoint : MonoBehaviour
{
    public TextMeshProUGUI hint;
    bool isAllowedToSave;
    SaveManager saveManager;
    public GameObject player;
    public GameObject spider;

    bool activatedFirst;

    public int orderNumber;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        activatedFirst = saveManager.saveInfo.screens[orderNumber - 1];
    }

    void Update()
    {
        if(isAllowedToSave && Input.GetKeyDown(KeyCode.E))
        {
            saveManager.saveInfo.playerPosition = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
            if(!activatedFirst)
            {
                saveManager.saveInfo.screens[orderNumber - 1] = true;
            }

            saveManager.SaveInfoToFile();

            if(!activatedFirst)
            {
                SceneManager.LoadScene("LoadScreen " + orderNumber);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Save");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(true);
            isAllowedToSave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(false);
            isAllowedToSave = false;
        }
    }
}