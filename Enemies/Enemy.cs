using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    [SerializeField]
    private int health;

    public GameObject deathEffect;



    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        Debug.Log(health);
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
