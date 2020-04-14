using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public GameObject deathEffect;

    public int Health { get; set; }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Die();
        }
        else
        {
            //покраснеть
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            Die();
        }
    }
}
