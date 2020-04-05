using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTunnel : MonoBehaviour
{

    public float speed;

    public bool trap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            trap = true;
        }
    }

    private void Update()
    {
        if(trap)
        {
            transform.Find("LongPlatformUp").transform.Translate(0, Time.deltaTime * speed, 0);
        }
    }

}
