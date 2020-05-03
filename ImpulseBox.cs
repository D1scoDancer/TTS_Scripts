using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseBox : MonoBehaviour
{
    public float forceX;
    public float forceY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY));
    }
}
