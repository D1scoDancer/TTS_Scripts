using UnityEngine;

/// <summary>
/// Дополнительный класс отвечающий за работу ловушки "Тунель смерти"
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