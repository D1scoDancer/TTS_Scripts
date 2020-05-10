using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public TextMeshProUGUI hint;
    bool isAllowedToSave;
    SaveManager saveManager;
    public GameObject player;
    public GameObject spider;

    bool activatedFirst;

    public int orderNumber;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        activatedFirst = saveManager.saveInfo.screens[orderNumber - 1];
    }

    // Update is called once per frame
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
