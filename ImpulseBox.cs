using UnityEngine;

public class ImpulseBox : MonoBehaviour
{
    public float forceY;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
    }
}
