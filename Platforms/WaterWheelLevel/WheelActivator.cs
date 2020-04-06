using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
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
