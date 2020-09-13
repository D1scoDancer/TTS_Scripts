using UnityEngine;

/// <summary>
/// Class describing spider's bullet
/// </summary>
public class SpiderBullet : MonoBehaviour
{
    public float speed = 60f;
    public int damage;
    public GameObject impactEffect;
    public GameObject player;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        player?.TakeDamage(damage);

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}