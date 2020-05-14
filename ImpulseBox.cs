using UnityEngine;

/// <summary>
/// Класс представляющий платформу придающую вектор силы к игроку
/// </summary>
public class ImpulseBox : MonoBehaviour
{
    public float forceY;

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
    }
}