using UnityEngine;

/// <summary>
/// Боевка Spider
/// </summary>
public class SpiderFighter : MonoBehaviour
{
    static System.Random rand = new System.Random();
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;
    private SpiderController spiderController;

    [SerializeField]
    private Player player;  // чтобы отслеживать положение противника

    public bool facingRight;
    public float speed;
    public int collideDamage;
    private float distance;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        attack = true;
        if(collision.gameObject.name == "Player")
        {
            collision.transform.GetComponent<Player>().TakeDamage(collideDamage);
            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
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
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
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
            Instantiate(bulletPrefab, firePointExtra1.position, firePointExtra1.rotation);
            Instantiate(bulletPrefab, firePointExtra2.position, firePointExtra2.rotation);
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
        if(Mathf.Abs(distance) < 20)
        {
            Shoot2();
        }
        else
        {
            Shoot1();
        }
    }

    private void ResetValues()
    {
        attack = false;
        shoot = false;
    }
}

