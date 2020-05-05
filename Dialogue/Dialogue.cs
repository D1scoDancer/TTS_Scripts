using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 6)]
    public string[] sentences;
}
