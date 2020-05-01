using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveInformation
{
    private static SaveInformation instance;
    private SaveInformation()
    { }

    public static SaveInformation getInstance()
    {
        if(instance == null)
            instance = new SaveInformation();
        return instance;
    }
    public int PlayerHealth { get; set; }
    public int SpiderHealth { get; set; }

    public float[] playerPosition = new float[3];
}
