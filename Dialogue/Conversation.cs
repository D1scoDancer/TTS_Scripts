using System;
using UnityEngine;

/// <summary>
/// Класс описывающий диалог между 2мя персонажами. Объект для хранения информации
/// </summary>
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}

/// <summary>
/// Структура описывающая реплики персонажа
/// </summary>
[Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;
}