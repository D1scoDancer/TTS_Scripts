using UnityEngine;

/// <summary>
/// Class that disables the player's ability to shoot
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