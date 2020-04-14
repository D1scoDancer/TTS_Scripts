using UnityEngine;

/// <summary>
/// Смена анимаций в SpiderController
/// </summary>
public class SpiderController : MonoBehaviour
{
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;

    private bool attack;
    private bool shoot;

    private void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void HandleAnimations()
    {

    }

    private void ResetValues()
    {
        // не знаю понадобится ли
    }
}
