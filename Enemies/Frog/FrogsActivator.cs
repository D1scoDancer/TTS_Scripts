using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogsActivator : MonoBehaviour
{
    SaveInformation saveInfo = SaveInformation.getInstance();
    private void Start()
    {
        if(saveInfo.FrogsKilled)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(Transform frog in transform)
        {
            if(frog.name.Contains("Frog"))
            {
                frog.GetComponent<FrogController>().enabled = true;
            }
        }
    }
}
