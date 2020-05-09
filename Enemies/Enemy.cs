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
                SaveInformation.ReadInfoFromFile();
            }
            saveInfo = SaveInformation.getInstance();

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }

        if(gameObject.name == "Spider")
        {
            Debug.Log(health);
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
