using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;

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

    private bool facingRight;
    private bool attack;
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private bool airControl;



    /// <summary>
    /// Начинает работу в начале сцены
    /// </summary>
    private void Start()
    {
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
        Flip(horizontal);
        HandleAttacks();
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
        if(isGrounded || airControl)
        {
            myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        }
        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    /// <summary>
    /// Атаки
    /// </summary>
    private void HandleAttacks()
    {
        if(attack)
        {
            myAnimator.SetTrigger("attack");  
        }
    }


    /// <summary>
    /// Ввод
    /// </summary>
    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            attack = true;
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
    private bool IsGrounded()
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
        attack = false;
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
