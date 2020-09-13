using UnityEngine;

/// <summary>
/// Class representing a moving platform
/// </summary>
public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    float speed;

    [SerializeField]
    bool activatedByPlayer;

    [SerializeField]
    bool movingRight;

    [SerializeField]
    float xDirection;

    [SerializeField]
    float yDirection;

    bool stop;

    void Update()
    {
        if(!stop)
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Turn"))
        {
            movingRight = !movingRight;
        }
        if(collision.gameObject.CompareTag("Stop"))
        {
            stop = true;
        }
        if(collision.gameObject.CompareTag("Rotate"))
        {
            stop = true;
            if(transform.Find("Player"))
            {
                transform.Find("Player").parent = null;
            }
            transform.Rotate(0, 0, -90);
        }
    }

    /// <summary>
    /// Start moving
    /// </summary>
    void Move()
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
}