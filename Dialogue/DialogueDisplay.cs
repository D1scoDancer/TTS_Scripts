using System.Collections;
using System.IO;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    public Plot plot;
    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    public DialogueActivator activator;

    SaveInformation saveInfo;

    private void Start()
    {
        saveInfo = SaveInformation.getInstance();
        if(File.Exists(Application.persistentDataPath + @"\saveFile.bin"))
        {
            saveInfo.ReadInfoFromFile();
            saveInfo = SaveInformation.getInstance();
        }
        conversation = plot.plot[saveInfo.dialogNumber];

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;

        AdvanceConversation();

        saveInfo.dialogNumber++;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AdvanceConversation();
        }
    }

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
            if(saveInfo.dialogNumber <= 2)
            {
                FindObjectOfType<AudioManager>().Stop("MainTheme");
                FindObjectOfType<AudioManager>().Play("BossBattle");
            }
        }
    }

    private void DisplayLine()
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

    private void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text)
    {
        inactiveSpeakerUI.Hide();
        activeSpeakerUI.Show();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(activeSpeakerUI, text));
    }

    private IEnumerator TypeSentence(SpeakerUI activeSpeakerUI, string text)
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
