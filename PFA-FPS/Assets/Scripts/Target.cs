
using System;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float maxHealth = 50f;
    public float currentHealth;
    
    public UIHealthBar healthBar;
   

    private void Awake()
    {
       
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amountDamage)
    {
        currentHealth -= amountDamage;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        
      
        if (currentHealth <= 0f )
        {
            Die();
            healthBar.gameObject.SetActive(false);
        }
        

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    
}
