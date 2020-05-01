using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IKillable
{
    public GameObject deathEffect;

    public int health;
    bool hit;

    SpriteRenderer spriteRenderer;
    PlayerController playerController;
    Rigidbody2D rigidbody2D;
    BoxCollider2D collider2D;
    Animator animator;
    BetterJump betterJump;
    Weapon weapon;
    SaveInformation saveInfo;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        betterJump = GetComponent<BetterJump>();
        weapon = GetComponent<Weapon>();

        if(File.Exists(Application.persistentDataPath + @"\saves\saveFile.bin"))
        {
            saveInfo = SaveInformation.getInstance();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using(FileStream fs = new FileStream(Application.persistentDataPath + @"\saves\saveFile.bin", FileMode.Open))
                {
                    saveInfo = (SaveInformation)formatter.Deserialize(fs);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log("exception on respawn");
            }
            transform.position = new Vector3(saveInfo.playerPosition[0], saveInfo.playerPosition[1], saveInfo.playerPosition[2]);
            health = saveInfo.PlayerHealth;
        }
        
       
    }
    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        DisableComponents();
        StartCoroutine("Respawn");
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(4); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisableComponents()
    {
        spriteRenderer.enabled = false;
        playerController.enabled = false;
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.constraints = (RigidbodyConstraints2D)7;
        collider2D.enabled = false;
        animator.enabled = false;
        betterJump.enabled = false;
        weapon.enabled = false;
    }

    private void EnableComponents()
    {
        spriteRenderer.enabled = true;
        playerController.enabled = true;
        rigidbody2D.gravityScale = 1f;
        rigidbody2D.constraints = (RigidbodyConstraints2D)4;
        collider2D.enabled = true;
        animator.enabled = true;
        betterJump.enabled = true;
        weapon.enabled = true;
    }

    private void Update()
    {
        if(hit)
        {
            FlashingRed();
        }
        ResetValues();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        hit = true;
    }

    private void FlashingRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(whitecolor());
    }

    IEnumerator whitecolor()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        hit = false;
    }

    private void ResetValues()
    {
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            Die();
        }
    }
}
