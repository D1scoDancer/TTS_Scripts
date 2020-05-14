﻿using TMPro;
using UnityEngine;

/// <summary>
/// Класс отображающий игровые подсказки
/// </summary>
public class TutorialMessage : MonoBehaviour
{
    public TextMeshProUGUI hint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(false);
        }
    }
}