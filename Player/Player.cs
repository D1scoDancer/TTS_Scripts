using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс описывающий сущность игрока
/// </summary>
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

    SaveManager saveManager;

    void Start()
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

    void Update()
    {
        if(hit)
        {
            FlashingRed();
        }
        ResetValues();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            Die();
        }
    }

    /// <summary>
    /// Умереть
    /// </summary>
    public void Die()
    {
        health = 0;
        FindObjectOfType<AudioManager>().Play("Death");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        DisableComponents();
        StartCoroutine("Respawn");
    }

    /// <summary>
    /// Возродиться
    /// </summary>
    /// <returns>время ожидания</returns>
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

    /// <summary>
    /// Отключить компоненты
    /// </summary>
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

    /// <summary>
    /// Получить урон
    /// </summary>
    /// <param name="damage">урон</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        hit = true;
    }

    /// <summary>
    /// Менять цвет на красный при получении урона
    /// </summary>
    void FlashingRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(WhiteColor());
    }

    /// <summary>
    /// Вернуть обычный цвет
    /// </summary>
    /// <returns>задержка</returns>
    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        hit = false;
    }

    /// <summary>
    /// Сбросить значения переменных
    /// </summary>
    void ResetValues()
    {
        hit = false;
    }
}