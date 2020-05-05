using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    public Conversation conversation;

    public Text nameL;
    public Text nameR;
    public Text textL;
    public Text textR;

    public void StartConversation()
    {

        nameL.text = conversation.speakerLeft.name;
        nameR.text = conversation.speakerRight.name;
        Debug.Log("Ничего не произошло");
    }
}
