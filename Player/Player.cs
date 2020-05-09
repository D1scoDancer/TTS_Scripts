using System.Collections;
using System.IO;
using UnityEngine;
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
        saveInfo = SaveInformation.getInstance();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        betterJump = GetComponent<BetterJump>();
        weapon = GetComponent<Weapon>();

        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveInfo.ReadInfoFromFile();
            saveInfo = SaveInformation.getInstance();

            transform.position = new Vector3(saveInfo.playerPosition[0] + 10, saveInfo.playerPosition[1], saveInfo.playerPosition[2]);
            health = saveInfo.PlayerHealth;
        }
    }
    public void Die()
    {
        health = 0;
        FindObjectOfType<AudioManager>().Play("Death");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        DisableComponents();
        StartCoroutine("Respawn");
    }

    public IEnumerator Respawn()
    {
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveInfo = SaveInformation.getInstance();
            saveInfo.SaveInfoToFile();
        }
        yield return new WaitForSeconds(4);

        saveInfo = SaveInformation.getInstance();
        if(saveInfo.dialogNumber == 1)
        {
            FindObjectOfType<AudioManager>().Stop("BossBattle");
            FindObjectOfType<AudioManager>().Play("MainTheme");
        }

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
        StartCoroutine(WhiteColor());
    }

    IEnumerator WhiteColor()
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
