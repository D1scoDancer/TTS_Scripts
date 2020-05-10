using UnityEngine;
using System.IO;

public class Enemy : MonoBehaviour, IKillable
{
    public int health;

    public GameObject deathEffect;

    SaveManager saveManager;

    private void Start()
    {
        if(gameObject.name == "Spider")
        {
            if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
            {
                saveManager = FindObjectOfType<SaveManager>();
                health = saveManager.saveInfo.SpiderHealth;
            }
        }
    }

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
        if(gameObject.name == "Spider")
        {
            FindObjectOfType<AudioManager>().Stop("BossBattle");
        }
    }
}
