using UnityEngine;

/// <summary>
/// Класс описывающий боевку паука
/// </summary>
public class SpiderFighter : MonoBehaviour
{
    static Random rand = new Random();
    Rigidbody2D spiderRigidbody2D;
    Animator spiderAnimator;
    SpiderController spiderController;

    [SerializeField]
    Player player;  // чтобы отслеживать положение противника

    public bool facingRight;
    public float speed;
    public int collideDamage;
    float distance;

    // стрельба
    public Transform firePoint;
    public Transform firePointExtra1;
    public Transform firePointExtra2;
    public GameObject bulletPrefab;
    public float fireRate;
    float nextFire;

    public bool shoot;
    public bool attack;

    void Start()
    {
        nextFire = Time.time;
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
        spiderController = GetComponent<SpiderController>();
    }

    void Update()
    {
        distance = player.transform.position.x - transform.position.x;
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(distance) > 10)
        {
            spiderRigidbody2D.velocity = Vector2.right * speed * (facingRight ? 1 : -1);
            Fight();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        attack = true;
        if(collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(collideDamage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
        }
    }

    /// <summary>
    /// Смотреть в сторону игрока
    /// </summary>
    void LookAtPlayer()
    {
        if(distance > 10 && !facingRight)
        {
            Flip();
        }
        else if(distance < -10 && facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Развернуться
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// Выстрелить способом 1
    /// </summary>
    void Shoot1()
    {
        if(CheckIfTimeToFire())
        {
            shoot = true;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    /// <summary>
    /// Выстрелить способом 2
    /// </summary>
    void Shoot2()
    {
        if(CheckIfTimeToFire())
        {
            shoot = true;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(bulletPrefab, firePointExtra1.position, firePointExtra1.rotation);
            Instantiate(bulletPrefab, firePointExtra2.position, firePointExtra2.rotation);
        }
    }

    /// <summary>
    /// Проверка можно ли стрелять (зависит от времени)
    /// </summary>
    /// <returns></returns>
    bool CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Начать бой
    /// </summary>
    void Fight()
    {
        if(Mathf.Abs(distance) < 20)
        {
            Shoot2();
        }
        else
        {
            Shoot1();
        }
    }

    /// <summary>
    /// Сбросить значения переменных
    /// </summary>
    void ResetValues()
    {
        attack = false;
        shoot = false;
    }
}