using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    public int Health { get; set; }

    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
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
