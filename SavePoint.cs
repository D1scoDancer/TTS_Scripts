using TMPro;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public TextMeshProUGUI hint;
    bool isAllowedToSave;
    SaveInformation saveInfo;
    public GameObject player;
    public GameObject spider;

    // Start is called before the first frame update
    void Start()
    {
        saveInfo = SaveInformation.getInstance();
        saveInfo.SpiderHealth = spider.GetComponent<Enemy>().health;
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
            saveInfo.SaveInfoToFile();
            FindObjectOfType<AudioManager>().Play("Save");
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
