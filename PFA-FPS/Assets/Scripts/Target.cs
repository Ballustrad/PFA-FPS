
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

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
    //Déclare une variable booléenne qui indique si la cible est paralysée
    bool isParalyzed = false;

    // La fonction pour paralyser la cible pendant une durée spécifiée
    public void Paralyze(float duration)
    {
        // Vérifie si la cible n'est pas déjà paralysée
        if (!isParalyzed)
        {
            // Définit la variable isParalyzed à true
            isParalyzed = true;

            // Arrête le mouvement de la cible
            GetComponent<Rigidbody>().velocity = Vector3.zero;



            // Lance une coroutine pour annuler la paralysie après une durée spécifiée
            StartCoroutine(CancelParalysis(duration));
        }
    }

    // La coroutine pour annuler la paralysie
    IEnumerator CancelParalysis(float duration)
    {
        // Attend la durée spécifiée
        yield return new WaitForSeconds(duration);



        // Rétablit le mouvement de la cible
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Réinitialise la variable isParalyzed
        isParalyzed = false;
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    
}
