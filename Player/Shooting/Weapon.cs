using System.Collections;
using UnityEngine;

/// <summary>
/// Class that allows the player to shoot
/// </summary>
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && spriteRenderer.color.b > 0.5)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
            Shoot();
        }
    }

    /// <summary>
    /// Return to normal color
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = new Color(255, 255, spriteRenderer.color.b + 0.1f, 255);
    }

    /// <summary>
    /// Shooting logic
    /// </summary>
    void Shoot()
    {
        spriteRenderer.color = new Color(255, 255, spriteRenderer.color.b - 0.1f, 255);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine("ReturnColor");
    }
}