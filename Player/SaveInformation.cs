using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveInformation 
{
    public int PlayerHealth { get; set; }
    public int SpiderHealth { get; set; }
    public Vector3 PlayerPosition { get; set; }
}
