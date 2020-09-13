using System;
using UnityEngine;

/// <summary>
/// Class describing the dialogue between 2 characters. Object for storing information
/// </summary>
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}

/// <summary>
/// Structure describing the character's lines
/// </summary>
[Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;
}