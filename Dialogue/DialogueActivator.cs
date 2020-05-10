using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    SaveManager saveManager;
    public GameObject dialogue;
    public PlayerController playerController;
    public SpiderFighter spiderFighter;
    public GameObject dissableWeapon;
    bool called;

    public void StartDialogue()
    {
        dialogue.GetComponent<DialogueDisplay>().enabled = true;
        playerController.enabled = false;
    }

    public void StartFight()
    {
        playerController.gameObject.GetComponent<Weapon>().enabled = true;
        playerController.enabled = true;
        spiderFighter.enabled = true;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!called && saveManager.saveInfo.dialogNumber < 2)
        {
            called = true;
            playerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerController.gameObject.GetComponent<Animator>().SetFloat("speed", 0);
            StartDialogue();
        }
    }

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if(saveManager.saveInfo.dialogNumber >= 2)
        {
            spiderFighter.gameObject.GetComponent<SpiderWaiter>().enabled = true;
            dissableWeapon.SetActive(false);
            playerController.gameObject.GetComponent<Weapon>().enabled = true;
        }
    }
}
