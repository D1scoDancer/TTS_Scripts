using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

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
        saveInfo = new SaveInformation();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAllowedToSave && Input.GetKeyDown(KeyCode.F))
        {
            saveInfo.playerPosition = new float[]{ player.transform.position.x, player.transform.position.y, player.transform.position.z };
            saveInfo.PlayerHealth = player.GetComponent<Player>().health;
            saveInfo.SpiderHealth = spider.GetComponent<Enemy>().health;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saves\saveFile.bin", FileMode.Create))
                {
                    formatter.Serialize(fs, saveInfo);
                }
                Debug.Log("Saved");
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log("exception on serializing");
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
