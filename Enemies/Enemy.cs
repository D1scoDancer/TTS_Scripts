using UnityEngine;
using System.IO;

public class Enemy : MonoBehaviour, IKillable
{
    public int health;

    public GameObject deathEffect;

    SaveInformation saveInfo;

    private void Start()
    {
        if(gameObject.name == "Spider")
        {
            saveInfo = SaveInformation.getInstance();
            if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
            {
                saveInfo.ReadInfoFromFile();
            }
            saveInfo = SaveInformation.getInstance();

            health = saveInfo.SpiderHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(health);
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        saveInfo.SpiderHealth = health;
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
