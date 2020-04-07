using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public double life = 1f;
    void  Start()
    {
        life = Time.time + 1.0;
    }

    void Update()
    {
        if(life <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
