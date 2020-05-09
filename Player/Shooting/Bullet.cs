using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage;
    public GameObject impactEffect;
    public GameObject player;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy?.TakeDamage(damage);

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
