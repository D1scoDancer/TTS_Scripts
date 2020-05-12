using System;
using UnityEngine;

/// <summary>
/// Класс описывающий поведение лягушки
/// </summary>
public class FrogController : MonoBehaviour
{
    Animator myAnimator;

    [SerializeField]
    float speed;

    [SerializeField]
    float sittingDuration;

    bool movingRight;
    bool moving;

    DateTime freezeTime;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        moving = myAnimator.GetBool("moving");
    }

    void Update()
    {
        if(movingRight && moving)
        {
            transform.Translate(Time.deltaTime * speed, speed * Time.deltaTime, 0);
            transform.localScale = new Vector3(-20, 20, 0);
        }
        else if(moving)
        {
            transform.Translate(-Time.deltaTime * speed, speed * Time.deltaTime, 0);
            transform.localScale = new Vector3(20, 20, 0);
        }
        else if(Math.Abs(freezeTime.Second - DateTime.Now.Second) >= sittingDuration)
        {
            moving = true;
            myAnimator.SetBool("moving", moving);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            GetComponent<Enemy>().Die();
        }
        if(collision.gameObject.CompareTag("Turn"))
        {
            movingRight = !movingRight;
        }
        moving = false;
        myAnimator.SetBool("moving", moving);
        freezeTime = DateTime.Now;
    }
}