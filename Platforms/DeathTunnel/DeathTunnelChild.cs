using UnityEngine;

/// <summary>
/// Additional class responsible for the operation of the "Tunnel of death" trap
/// </summary>
public class DeathTunnelChild : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
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