using Cinemachine;
using UnityEngine;

/// <summary>
/// Класс контролирующий передвижение игрока и смену анимаций игрока
/// </summary>
public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    public Transform cameraCM;

    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    Transform[] groundPoints;

    [SerializeField]
    float speed = 0; // скорость движения по x
    [SerializeField]
    float groundRadius;
    [SerializeField]
    float jumpForce;

    // настройки камеры
    float cmYStandart;
    [SerializeField]
    float cmYDown;
    [SerializeField]
    float cmSpeed;
    bool cameraDown;

    bool facingRight;
    bool isGrounded;
    bool jump;
    bool climb;
    [SerializeField]
    bool airControl;

    void Start()
    {
        cmYStandart = cameraCM.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        if(!climb)
        {
            HandleCamera();
        }
        Flip(horizontal);
        HandleLayers();
        ResetValues();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }

    /// <summary>
    /// Движение
    /// </summary>
    /// <param name="horizontal"></param>
    void HandleMovement(float horizontal)
    {
        if(myRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if(isGrounded || (airControl)) //движение в воздухе, да и просто
        {
            myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        }
        if(isGrounded && jump)  // прыжок
        {
            isGrounded = false;
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    /// <summary>
    /// Обработка взодныз данных
    /// </summary>
    void HandleInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        if(Input.GetKey(KeyCode.S))
        {
            cameraDown = true;
        }
        else
        {
            cameraDown = false;
        }
    }

    /// <summary>
    /// Работа с камерой
    /// </summary>
    void HandleCamera()
    {
        var setting = cameraCM.GetComponent<CinemachineVirtualCamera>();
        var body = setting.GetCinemachineComponent<CinemachineFramingTransposer>();
        if(cameraDown)
        {
            if(body.m_ScreenY > cmYDown)
            {
                body.m_ScreenY -= cmSpeed * 0.05f;
            }
        }
        else
        {
            if(body.m_ScreenY < cmYStandart)
            {
                body.m_ScreenY += cmSpeed * 0.05f;
            }
        }
    }

    /// <summary>
    /// Поворот персонажа
    /// </summary>
    /// <param name="horizontal"></param>
    void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            transform.Rotate(0, 180, 0);
        }
    }

    /// <summary>
    /// Проверка стоит ли персонаж на земле
    /// Сталкивются ли точки с чем то кроме персонажа
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        if(myRigidbody2D.velocity.y <= 0)
        {
            foreach(var point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                foreach(var collider in colliders)
                {
                    if(collider.gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Смена слоев анимации
    /// </summary>
    void HandleLayers()
    {
        if(!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    /// <summary>
    /// Сброс значений
    /// </summary>
    void ResetValues()
    {
        jump = false;
        cameraDown = false;
    }
}