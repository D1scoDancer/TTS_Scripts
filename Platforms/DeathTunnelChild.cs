using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTunnelChild : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(1);
        if(collision.gameObject.name == "LongPlatformDown")
        {
            transform.parent.GetComponent<DeathTunnel>().trap = false;
        }
    }
}

