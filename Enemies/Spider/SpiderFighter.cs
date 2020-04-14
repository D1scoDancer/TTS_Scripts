using UnityEngine;

/// <summary>
/// Боевка Spider
/// </summary>
public class SpiderFighter : MonoBehaviour, IDamagable
{
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;

    [SerializeField]
    private Player player;  // чтобы отслеживать положение противника
    [SerializeField]
    private GameObject spiderBullet;

    private bool facingRight;
    public float speed;
    public int CollideDamage { get; set; }

    void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }



    public void onCollisionrEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().TakeDamage(CollideDamage);
            //сделать таймаут получения урона
        }
    }
}
