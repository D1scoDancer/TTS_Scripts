using UnityEngine;

/// <summary>
/// Боевка Spider
/// </summary>
public class SpiderFighter : MonoBehaviour
{
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;
    private SpiderController spiderController;

    [SerializeField]
    private Player player;  // чтобы отслеживать положение противника

    private bool facingRight;
    public float speed;
    public int collideDamage;
    private float distance;

    // стрельба
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate;
    float nextFire = Time.time;

    public bool shoot;
    public bool attack;



    void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
        spiderController = GetComponent<SpiderController>();
        facingRight = true;

    }

    void Update()
    {
        distance = player.transform.position.x - transform.position.x;
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(distance) > 75)
        {
            spiderRigidbody2D.velocity = Vector2.right * speed * (facingRight ? 1 : -1);
        }
        else
        {
            Fight();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().TakeDamage(collideDamage);
        }
    }

    /// <summary>
    /// Всегда смотрит в сторону игрока
    /// </summary>
    private void LookAtPlayer()
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
    /// Разворот
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Attack()
    {
        attack = true;
    }

    private void Shoot1()
    {
        if(CheckIfTimeToFire())
        {
            shoot = true;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void Shoot2()
    {
        if(CheckIfTimeToFire())
        {
            shoot = true;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private bool CheckIfTimeToFire()
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

    private void Fight()
    {
        spiderRigidbody2D.velocity = Vector2.zero;
        if(Mathf.Abs(distance) < 5)
        {

            Attack();
            Strafe();
        }
        else if(Mathf.Abs(distance) < 20)
        {
            Shoot2();
            Strafe();
        }
        else
        {
            Shoot1();
            Strafe();
        }
    }

    /// <summary>
    /// Отходить от слизи после действия
    /// </summary>
    private void Strafe()
    {
       // spiderRigidbody2D.velocity = Vector2.left * speed * (facingRight ? 1 : -1);
    }

    private void ResetValues()
    {
        attack = false;
        shoot = false;
    }

}

