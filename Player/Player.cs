using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IKillable
{
    public GameObject deathEffect;

    public int health;
    private bool hit;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D collider2D;
    private Animator animator;
    private BetterJump betterJump;
    private Weapon weapon;

    private SaveManager saveManager;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        betterJump = GetComponent<BetterJump>();
        weapon = GetComponent<Weapon>();

        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveManager = FindObjectOfType<SaveManager>();
            transform.position = new Vector3(saveManager.saveInfo.playerPosition[0] + 10, saveManager.saveInfo.playerPosition[1], saveManager.saveInfo.playerPosition[2]);
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
            saveManager.saveInfo.SpiderHealth = FindObjectOfType<SpiderController>().gameObject.GetComponent<Enemy>().health;
            saveManager.SaveInfoToFile();
        }
        yield return new WaitForSeconds(4);

        if(saveManager.saveInfo.dialogNumber == 1)
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