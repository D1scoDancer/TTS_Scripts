using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogController : MonoBehaviour
{
    [SerializeField]
    Player player;

    Rigidbody2D rigidbody;

    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float rangeOfWalking;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if(Mathf.Abs(rigidbody.position.x - player.GetXPosition()) <= 300)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

    }

    void Move()
    {


    }






    void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
