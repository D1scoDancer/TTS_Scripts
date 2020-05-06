using TMPro;
using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    public TextMeshProUGUI hint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(false);
        }
    }
}