using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTunnelChild : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(collision.transform.GetComponent<PlayerController>().IsGrounded())
            {
                collision.transform.GetComponent<Player>().Die();
            }
        }

        if(collision.gameObject.name == "LongPlatformDown")
        {
            transform.parent.GetComponent<DeathTunnel>().trap = false;
        }
    }
}

