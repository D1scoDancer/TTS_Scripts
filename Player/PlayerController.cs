using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    public Transform cameraCM;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float speed = 0; // скорость движения по x
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private float jumpForce;

    // настройки камеры
    private float cmYStandart;
    [SerializeField]
    private float cmYDown;
    [SerializeField]
    private float cmSpeed;
    private bool cameraDown;

    private bool facingRight;
    private bool isGrounded;
    private bool jump;
    private bool climb;
    [SerializeField]
    private bool airControl;


    /// <summary>
    /// Начинает работу в начале сцены
    /// </summary>
    private void Start()
    {
        cmYStandart = cameraCM.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Обновляется каждый кадр
    /// </summary>
    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Обновляется каждую единицу времени
    /// </summary>
    private void FixedUpdate()
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

    /// <summary>
    /// Движение
    /// </summary>
    /// <param name="horizontal"></param>
    private void HandleMovement(float horizontal)
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
    /// Ввод
    /// </summary>
    private void HandleInput()
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

    private void HandleCamera()
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
    private void Flip(float horizontal)
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

    private void HandleLayers()
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
    private void ResetValues()
    {
        jump = false;
        cameraDown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }

    public float GetXPosition()
    {
        return myRigidbody2D.position.x;
    }
}
