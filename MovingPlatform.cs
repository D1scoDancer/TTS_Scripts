using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    float speed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Time.deltaTime * speed, Time.deltaTime * speed, 0);
    }
}
