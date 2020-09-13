using UnityEngine;

/// <summary>
/// Class responsible for starting a dialog
/// </summary>
public class DialogueActivator : MonoBehaviour
{
    SaveManager saveManager;
    public GameObject dialogue;
    public PlayerController playerController;
    public SpiderFighter spiderFighter;
    public GameObject dissableWeapon;
    bool called;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if(saveManager.saveInfo.dialogNumber >= 2)
        {
            spiderFighter.gameObject.GetComponent<SpiderWaiter>().enabled = true;
            dissableWeapon.SetActive(false);
            playerController.gameObject.GetComponent<Weapon>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!called && saveManager.saveInfo.dialogNumber < 2)
        {
            called = true;
            playerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerController.gameObject.GetComponent<Animator>().SetFloat("speed", 0);
            StartDialogue();
        }
    }

    /// <summary>
    /// Start a dialogue
    /// </summary>
    public void StartDialogue()
    {
        dialogue.GetComponent<DialogueDisplay>().enabled = true;
        playerController.enabled = false;
    }

    /// <summary>
    /// Start battle (after dialogue ends)
    /// </summary>
    public void StartFight()
    {
        if(GameObject.Find("Spider") == null)
        {
            playerController.gameObject.GetComponent<Weapon>().enabled = true;
            playerController.enabled = true;
            this.enabled = false;
            FindObjectOfType<SpiderDeadBody>().gameObject.GetComponent<BoxCollider2D>().enabled = true;
            return;
        }

        playerController.gameObject.GetComponent<Weapon>().enabled = true;
        playerController.enabled = true;
        spiderFighter.enabled = true;
        this.enabled = false;
    }
}