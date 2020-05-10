using System.Collections;
using UnityEngine;

public class EndingZone : MonoBehaviour
{
    [HideInInspector]
    public bool spiderDead;
    private bool playerIn;
    private bool calledOnce;
    public GameObject dialogue;
    public GameObject player;

    void Update()
    {
        if(spiderDead && playerIn && !calledOnce)
        {
            calledOnce = true;
            StartCoroutine(EndTheGame());
        }
    }

    IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(1f);
        StartDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerIn = false; ;
        }
    }

    public void StartDialogue()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Weapon>().enabled = false;
        dialogue.GetComponent<DialogueDisplay>().enabled = true; 
    }
}
