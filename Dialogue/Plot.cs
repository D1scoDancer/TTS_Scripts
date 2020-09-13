using UnityEngine;

/// <summary>
/// Class describing the plot. Object for storing data
/// </summary>
[CreateAssetMenu(fileName = "New Plot", menuName = "Plot")]
public class Plot : ScriptableObject
{
    public Conversation[] plot;
}