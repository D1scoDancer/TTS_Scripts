using UnityEngine;

/// <summary>
/// Class describing the character. Object for storing information
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite portrait;
}