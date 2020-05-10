﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderDeadBody : MonoBehaviour
{
    public EndingZone endingZone;
    public GameObject hint;
    bool active;

    private void Start()
    {
        endingZone = FindObjectOfType<EndingZone>();
        endingZone.spiderDead = true;
        hint = GameObject.Find("Canvas2").transform.GetChild(6).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(true);
            active = true; ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(false);
            active = false; ;
        }
    }

    private void Update()
    {
        if(active && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
