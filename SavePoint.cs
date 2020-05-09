using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public TextMeshProUGUI hint;
    bool isAllowedToSave;
    SaveInformation saveInfo;
    public GameObject player;
    public GameObject spider;

    private bool activatedFirst;

    public int orderNumber;


    // Start is called before the first frame update
    void Start()
    {
        saveInfo = SaveInformation.getInstance();
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            SaveInformation.ReadInfoFromFile();
            saveInfo = SaveInformation.getInstance();
        }
        activatedFirst = saveInfo.screens[orderNumber - 1];
    }
    // Update is called once per frame
    void Update()
    {
        if(isAllowedToSave && Input.GetKeyDown(KeyCode.E))
        {
            saveInfo.playerPosition = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
            saveInfo.PlayerHealth = player.GetComponent<Player>().health;
            saveInfo.SpiderHealth = spider.GetComponent<Enemy>().health;
            saveInfo.FrogsKilled = GameObject.FindGameObjectsWithTag("Frog").Length == 0;
            if(!activatedFirst)
            {
                saveInfo.screens[orderNumber - 1] = true;
            }
            Debug.Log("respawn" + saveInfo.SpiderHealth);
            SaveInformation.SaveInfoToFile(saveInfo);
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
