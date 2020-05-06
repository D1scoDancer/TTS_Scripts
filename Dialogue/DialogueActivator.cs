using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    public GameObject dialogue;


    public void StartDialogue()
    {
        dialogue.GetComponent<DialogueDisplay>().enabled = true;
    }
}
