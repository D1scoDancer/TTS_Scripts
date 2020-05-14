using UnityEngine;

/// <summary>
/// Класс отключающий возможность стрелять у игрока
/// </summary>
public class DissableWeapon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.GetComponent<Weapon>().enabled = false;
        }
    }
}