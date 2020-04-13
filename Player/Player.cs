using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public GameObject deathEffect;

    public int Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
    }
}
