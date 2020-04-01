using System;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Animator myAnimator;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float sittingDuration;

    private bool movingRight;
    private bool moving;

    private DateTime freezeTime;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        moving = myAnimator.GetBool("moving");
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Turn"))
        {
            movingRight = !movingRight;
        }
        moving = false;
        myAnimator.SetBool("moving", moving);
        freezeTime = DateTime.Now;
    }
}
