using System.Collections;
using System.IO;
using UnityEngine;

/// <summary>
/// Class responsible for the work of dialogs
/// </summary>
public class DialogueDisplay : MonoBehaviour
{
    public Plot plot;
    public Conversation conversation;
    public Conversation finalMonologue;
    public GameObject speakerLeft;
    public GameObject speakerRight;

    SpeakerUI speakerUILeft;
    SpeakerUI speakerUIRight;

    int activeLineIndex = 0;

    public DialogueActivator activator;

    SaveManager saveManager;

    void Start()
    {
        if(GameObject.Find("Spider") == null)
        {
            conversation = finalMonologue;

            speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
            speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

            speakerUILeft.Speaker = conversation.speakerLeft;
            speakerUIRight.Speaker = conversation.speakerRight;
            AdvanceConversation();
            return;
        }

        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveManager = FindObjectOfType<SaveManager>();
        }

        conversation = plot.plot[saveManager.saveInfo.dialogNumber];

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;

        saveManager.saveInfo.dialogNumber++;
        AdvanceConversation();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AdvanceConversation();
        }
    }

    /// <summary>
    /// Continue the dialogue (change speakers one by one)
    /// </summary>
    public void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex++;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0;
            activator.StartFight();
            this.enabled = false;
            if(GameObject.Find("Spider") != null && saveManager.saveInfo.dialogNumber <= 2)
            {
                FindObjectOfType<AudioManager>().Stop("MainTheme");
                FindObjectOfType<AudioManager>().Play("BossBattle");
            }
        }
    }

    /// <summary>
    /// Identify the speaker
    /// </summary>
    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if(speakerUILeft.SpeakerIs(character))
        {
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
        }
        else
        {
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }
    }

    /// <summary>
    /// Show the speaker's UI, hide the other's UI
    /// </summary>
    /// <param name="activeSpeakerUI">speaker</param>
    /// <param name="inactiveSpeakerUI">listener</param>
    /// <param name="text">speaker's line</param>
    void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text)
    {
        inactiveSpeakerUI.Hide();
        activeSpeakerUI.Show();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(activeSpeakerUI, text));
    }

    /// <summary>
    /// Outputting a string letter by letter with a delay
    /// </summary>
    /// <param name="activeSpeakerUI">speaker</param>
    /// <param name="text">speaker's line</param>
    /// <returns>letters with a delay of 3 frames</returns>
    IEnumerator TypeSentence(SpeakerUI activeSpeakerUI, string text)
    {
        activeSpeakerUI.Dialog = "";
        foreach(char letter in text.ToCharArray())
        {
            activeSpeakerUI.Dialog += letter;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        }
    }
}