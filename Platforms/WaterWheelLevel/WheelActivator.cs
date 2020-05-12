using UnityEngine;

/// <summary>
/// Класс отвечающий за активацию ловушки "Колесо"
/// </summary>
public class WheelActivator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            foreach(Transform son in transform.GetComponentsInChildren<Transform>())
            {
                foreach(var grandson in son.GetComponentsInChildren<WaterWheelPlatform>())
                {
                    grandson.activatedByPlayer = false;
                }
            }
        }
    }
}