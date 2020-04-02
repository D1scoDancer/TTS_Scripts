using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool activatedByPlayer;

    [SerializeField]
    private bool movingRight;

    [SerializeField]
    private int xDirection;

    [SerializeField]
    private int yDirection;

    private void Update()
    {
        if(activatedByPlayer)
        {
            if(transform.Find("Player") != null)
            {
                Move();
                activatedByPlayer = false;
            }
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        if(movingRight)
        {
            transform.Translate(Time.deltaTime * speed * xDirection,
                Time.deltaTime * speed * yDirection, 0);
        }
        else
        {
            transform.Translate(-Time.deltaTime * speed * xDirection,
                -Time.deltaTime * speed * yDirection, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Turn"))
        {
            movingRight = !movingRight;
        }
    }
}
