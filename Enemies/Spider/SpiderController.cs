using UnityEngine;

/// <summary>
/// Смена анимаций в SpiderController
/// </summary>
public class SpiderController : MonoBehaviour
{
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;
    private SpiderFighter spiderFighter;

    private void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
        spiderFighter = GetComponent<SpiderFighter>();
    }

    private void Update()
    {
        spiderAnimator.SetFloat("speed", Mathf.Abs(spiderRigidbody2D.velocity.x));
    }
}
