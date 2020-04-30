using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
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
            saveInfo.PlayerPosition = transform.position;
            saveInfo.PlayerHealth = ;
            saveInfo.SpiderHealth = ;

            try
            {

            }
            catch
            {
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
