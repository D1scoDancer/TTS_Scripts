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

        spiderRigidbody2D.velocity = Vector2.right * speed * (facingRight ? 1 : -1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().TakeDamage(collideDamage);
            //сделать таймаут получения урона

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
        spiderController.attack = true;
        collideDamage *= 2;
    }

    private void Shoot1()
    {

    }

    private void Shoot2()
    {

    }


    private void Fight()
    {
        if(Mathf.Abs(distance) < 75)
        {
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
    }



    /// <summary>
    /// Отходить от слизи иногда
    /// </summary>
    private void Strafe()
    {

    }

    private void ResetValues()
    {
        collideDamage /= 2;
    }

}

