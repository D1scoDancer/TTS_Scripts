using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField]
    int health;


    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if has tag damagable health--
    }

    void Die()
    {
        //play DeathAnimation
        Destroy(this);
    }
}
