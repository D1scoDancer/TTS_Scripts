using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AutoDestroy : MonoBehaviour
{
    DateTime t;

    public float timing = 1;

    void Start()
    {
        t = DateTime.Now;
    }

    void Update()
    {
        if((DateTime.Now - t).TotalSeconds >= timing)
        {
            Destroy(gameObject);
        }
    }
}
