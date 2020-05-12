using UnityEngine;

/// <summary>
/// Класс отвечающий за работу ловушки "Колесо"
/// </summary>
public class WaterWheelPlatform : MonoBehaviour
{
    [SerializeField]
    float speed;

    public bool activatedByPlayer;

    public bool movingRight;

    [SerializeField]
    [Range(-1, 1)]
    int xMultiplier;

    [SerializeField]
    [Range(-1, 1)]
    int yMultiplier;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WheelCommand") && collision.transform.parent == transform.parent)
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

    /// <summary>
    /// Начать движение "Колеса"
    /// </summary>
    void StartWheel()
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
}