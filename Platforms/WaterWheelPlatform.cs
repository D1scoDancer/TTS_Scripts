using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheelPlatform : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool activatedByPlayer;

    public bool movingRight;

    [Range(0, 1)]
    private int xMultiplier;

    [Range(0, 1)]
    private int yMultiplier;

    private void Start()
    {
        if(movingRight)
        {
            xMultiplier = 0;
            yMultiplier = 1;
        }
        else
        {
            xMultiplier = 0;
            yMultiplier = -1;
        }
    }

    void Update()
    {
        if(activatedByPlayer)
        {
            if(transform.Find("Player") != null)
            {
                StartWheel();
                activatedByPlayer = false;
            }
        }
        else
        {
            StartWheel();
        }
    }

    private void StartWheel()
    {
        if(movingRight)
        {
            transform.Translate(Time.deltaTime * speed * xMultiplier,
                Time.deltaTime * speed * yMultiplier, 0);
        }
        else
        {
            transform.Translate(Time.deltaTime * speed * xMultiplier,
                Time.deltaTime * speed * yMultiplier, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WheelCommand"))
        {
            if(movingRight)
            {
                switch(collision.gameObject.name)
                {
                    case "WheelCommand1":
                        xMultiplier = 1; yMultiplier = 0;
                        break;
                    case "WheelCommand2":
                        xMultiplier = 0; yMultiplier = -1;
                        break;
                    case "WheelCommand3":
                        xMultiplier = -1; yMultiplier = 0;
                        break;
                    case "WheelCommand4":
                        xMultiplier = 0; yMultiplier = 1;
                        break;
                }
            }
            else
            {
                switch(collision.gameObject.name)
                {
                    case "WheelCommand1":
                        xMultiplier = 0; yMultiplier = -1;
                        break;
                    case "WheelCommand2":
                        xMultiplier = -1; yMultiplier = 0;
                        break;
                    case "WheelCommand3":
                        xMultiplier = 0; yMultiplier = 1;
                        break;
                    case "WheelCommand4":
                        xMultiplier = 1; yMultiplier = 0;
                        break;
                }
            }
        }
    }
}
