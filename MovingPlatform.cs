using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float speed;
    
    private void Start()
    {
        
    }

    
    private void Update()
    {
        transform.Translate(Time.deltaTime * speed, -Time.deltaTime * speed, 0);
    }

    
}
