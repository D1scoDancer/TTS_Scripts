using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiderWaiter : MonoBehaviour
{
    public SpiderFighter spiderFighter;
    public GameObject player;
    void Start()
    {
        transform.position = new Vector3(3788f, 231.5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 200)
        {
            spiderFighter.enabled = true;
        }
        else
        {
            spiderFighter.enabled = false;
            transform.position = new Vector3(3788f, 231.5f, 10f);
        }
    }
}
