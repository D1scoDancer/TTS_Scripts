using UnityEngine;

/// <summary>
/// Класс описывающий персонажа. Объект для хранения информации
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite portrait;
}