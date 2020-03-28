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
    bool jumpAttack;



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
        HandleLayers();
        ResetValues();
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
        if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && (isGrounded || airControl)) //нельзя двигаться пока идет атака
        {
            myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);
        }
        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
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
        if(attack && isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody2D.velocity = Vector2.zero;
        }
        if(jumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", true);
        }
        if(!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", false);
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
            jumpAttack = true;
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
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

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
        attack = false;
        slide = false;
        jumpAttack = false;
    }
}
