using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class describing the UI of the dialogue participants
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
    /// Speaker check
    /// </summary>
    /// <param name="character"></param>
    /// <returns>result of checking</returns>
    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    /// <summary>
    /// Hide UI
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Show UI
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }
}