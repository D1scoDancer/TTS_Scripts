using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    public int health;

    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
