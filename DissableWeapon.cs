using UnityEngine;

public class DissableWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.GetComponent<Weapon>().enabled = false;
        }
    }
}