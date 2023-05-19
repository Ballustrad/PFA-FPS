
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
    //D�clare une variable bool�enne qui indique si la cible est paralys�e
    bool isParalyzed = false;

    // La fonction pour paralyser la cible pendant une dur�e sp�cifi�e
    public void Paralyze(float duration)
    {
        // V�rifie si la cible n'est pas d�j� paralys�e
        if (!isParalyzed)
        {
            // D�finit la variable isParalyzed � true
            isParalyzed = true;

            // Arr�te le mouvement de la cible
            GetComponent<Rigidbody>().velocity = Vector3.zero;



            // Lance une coroutine pour annuler la paralysie apr�s une dur�e sp�cifi�e
            StartCoroutine(CancelParalysis(duration));
        }
    }

    // La coroutine pour annuler la paralysie
    IEnumerator CancelParalysis(float duration)
    {
        // Attend la dur�e sp�cifi�e
        yield return new WaitForSeconds(duration);



        // R�tablit le mouvement de la cible
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        // R�initialise la variable isParalyzed
        isParalyzed = false;
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    
}
