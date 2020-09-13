using System;
using UnityEngine;

/// <summary>
/// Class for saving game progress
/// </summary>
[Serializable]
public class SaveInformation
{
    [HideInInspector]
    public int SpiderHealth { get; set; }

    [HideInInspector]
    public float[] playerPosition = new float[3];

    [HideInInspector]
    public bool FrogsKilled { get; set; }

    [HideInInspector]
    public int dialogNumber = 0;

    [HideInInspector]
    public bool[] screens = new bool[3];

    public override string ToString()
    {
        return $"SpiderHealth: {SpiderHealth}; FrogsKilled: {FrogsKilled}; DialogNumber: {dialogNumber}, screens: {screens[0]},{screens[1]},{screens[2]}";
    }
}