
using System;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public TextMeshProUGUI healthText;
    private void Awake()
    {
        healthText.text = health.ToString();
    }
    public void TakeDamage(float amountDamage)
    {
        health -= amountDamage;
        healthText.text = health.ToString();
        if (health <= 0f )
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
