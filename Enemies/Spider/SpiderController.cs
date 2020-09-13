using UnityEngine;

/// <summary>
/// Class describing the change of spider animations
/// </summary>
public class SpiderController : MonoBehaviour
{
    Rigidbody2D spiderRigidbody2D;
    Animator spiderAnimator;

    void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        spiderAnimator.SetFloat("speed", Mathf.Abs(spiderRigidbody2D.velocity.x));
    }
}