using System;
using UnityEngine;

/// <summary>
/// Class responsible for the destruction of the game object
/// </summary>
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