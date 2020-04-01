using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool movingRight;

    [SerializeField]
    private int xDirection;

    [SerializeField]
    private int yDirection;



    private void Start()
    {

    }

    private void Update()
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
