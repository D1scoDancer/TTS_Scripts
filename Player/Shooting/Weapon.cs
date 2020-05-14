﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, позволяющий игроку стрелять
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
    /// Возврат обычного цвета
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = new Color(255, 255, spriteRenderer.color.b + 0.1f, 255);
    }

    /// <summary>
    /// Логика стрельбы
    /// </summary>
    void Shoot()
    {
        spriteRenderer.color = new Color(255, 255, spriteRenderer.color.b - 0.1f, 255);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine("ReturnColor");
    }
}