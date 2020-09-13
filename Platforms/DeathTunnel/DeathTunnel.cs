using UnityEngine;

/// <summary>
/// Class representing one of the "Death Tunnel" traps
/// </summary>
public class DeathTunnel : MonoBehaviour
{
    public float speed;
    public bool trap;

    void Update()
    {
        if(trap)
        {
            transform.Find("LongPlatformUp").transform.Translate(0, Time.deltaTime * speed, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            trap = true;
        }
    }
}