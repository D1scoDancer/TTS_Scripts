using UnityEngine;
using System.IO;

/// <summary>
/// Класс описывающий врага
/// </summary>
public class Enemy : MonoBehaviour, IKillable
{
    public int health;

    public GameObject deathEffect;
    public GameObject deadSpider;

    SaveManager saveManager;

    void Start()
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

    /// <summary>
    /// Получить урон
    /// </summary>
    /// <param name="damage">урон</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 20 && FindObjectOfType<Player>().health <= 15)
        {
            health += 40;
        }
        if(health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Умереть
    /// </summary>
    public void Die()
    {
        if(gameObject.name == "Spider")
        {
            Instantiate(deadSpider, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Stop("BossBattle");
            Destroy(gameObject);
            return;
        }
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}