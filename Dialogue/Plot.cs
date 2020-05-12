using UnityEngine;

/// <summary>
/// Класс описывающий сюжет. Объект для хранения данных
/// </summary>
[CreateAssetMenu(fileName = "New Plot", menuName = "Plot")]
public class Plot : ScriptableObject
{
    public Conversation[] plot;
}