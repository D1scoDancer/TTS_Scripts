using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс описывающий UI участников диалога
/// </summary>
public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public Text fullName;
    public Text dialog;

    Character speaker;

    public Character Speaker
    {
        get => speaker;
        set
        {
            speaker = value;
            portrait.sprite = speaker.portrait;
            fullName.text = speaker.fullName;
        }
    }

    public string Dialog
    {
        set { dialog.text = value; }
        get => dialog.text;
    }

    /// <summary>
    /// Проверка на говорящего
    /// </summary>
    /// <param name="character"></param>
    /// <returns>результат проверки</returns>
    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    /// <summary>
    /// Спрятать UI
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Показать UI
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }
}