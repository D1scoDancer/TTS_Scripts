using UnityEngine;

/// <summary>
/// Смена анимаций в SpiderController
/// </summary>
public class SpiderController : MonoBehaviour
{
    private Rigidbody2D spiderRigidbody2D;
    private Animator spiderAnimator;

    public bool attack;
    public bool shoot;

    private void Start()
    {
        spiderRigidbody2D = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimations();
        ResetValues();
    }

    private void HandleAnimations()
    {
        if(shoot)
        {
            spiderAnimator.SetBool("shoot", shoot);
        }
        if(attack)
        {
            spiderAnimator.SetBool("shoot", attack);
        }
        spiderAnimator.SetFloat("speed", Mathf.Abs(spiderRigidbody2D.velocity.x));
    }

    private void ResetValues()
    {
        shoot = false;
        attack = false;
    }
}
