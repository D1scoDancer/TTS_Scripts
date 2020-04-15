using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public GameObject deathEffect;

    public int health;
    SpriteRenderer spr;
    Color def;

    private void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        def = spr.color;
    }
    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("HIT");
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        else
        {
            spr.color = new Color(247, 47, 47);
            Debug.Log("before");
            Invoke("ReturnColor", 2);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            Die();
        }
    }

    private void ReturnColor()
    {
        spr.color = def;
        Debug.Log("after");
    }

}
