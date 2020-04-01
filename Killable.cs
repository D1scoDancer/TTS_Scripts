using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField]
    private int health;



    private void Start()
    {
        
    }


    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if has tag damagable health--
    }

    private void Die()
    {
        //play DeathAnimation
        Destroy(this);
    }
}
