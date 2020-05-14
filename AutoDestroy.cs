using System;
using UnityEngine;

/// <summary>
/// Класс отвечающий за уничтожение игрового объекта
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