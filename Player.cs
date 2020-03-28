using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;

    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    Transform[] groundPoints;

    [SerializeField]
    float movementSpeed = 0; // скорость движения по x
    [SerializeField]
    float groundRadius;
    [SerializeField]
    float jumpForce;

    bool facingRight;
    bool attack;
    bool slide;
    bool isGrounded;
    bool jump;
    [SerializeField]
    bool airControl;



    /// <summary>
    /// Начинает работу в начале сцены
    /// </summary>
    void Start()
    {
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Обновляется каждый кадр
    /// </summary>
    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Обновляется каждую единицу времени
    /// </summary>
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
        ResetValues();
    }

    /// <summary>
    /// Движение
    /// </summary>
    /// <param name="horizontal"></param>
    void HandleMovement(float horizontal)
    {
        if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && (isGrounded || airControl)) //нельзя двигаться пока идет атака
        {
            myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);
        }
        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
        if(slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    /// <summary>
    /// Атаки
    /// </summary>
    void HandleAttacks()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody2D.velocity = Vector2.zero;
        }
    }


    /// <summary>
    /// Ввод
    /// </summary>
    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
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

            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    /// <summary>
    /// Проверка стоит ли персонаж на земле
    /// Сталкивются ли точки с чем то кроме персонажа
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
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
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Сброс значений
    /// </summary>
    void ResetValues()
    {
        jump = false;
        attack = false;
        slide = false;
    }
}
