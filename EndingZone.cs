using System.Collections;
using UnityEngine;

/// <summary>
/// Класс реализующий концовку игры
/// </summary>
public class EndingZone : MonoBehaviour
{
    [HideInInspector]
    public bool spiderDead;
    bool playerIn;
    bool calledOnce;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerIn = true;
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerIn = false; ;
        }
    }

    /// <summary>
    /// Начать с задержкой финальный монолог
    /// </summary>
    /// <returns></returns>
    IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(1f);
        StartDialogue();
    }

    /// <summary>
    /// Финальынй монолог
    /// </summary>
    public void StartDialogue()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Weapon>().enabled = false;
        dialogue.GetComponent<DialogueDisplay>().enabled = true;
    }
}