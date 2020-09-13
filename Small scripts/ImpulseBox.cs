using UnityEngine;

/// <summary>
/// Class representing the platform giving the vector of force to the player
/// </summary>
public class ImpulseBox : MonoBehaviour
{
    public float forceY;

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
    }
}