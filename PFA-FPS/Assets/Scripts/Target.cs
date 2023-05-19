
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Timeline;

public class Target : MonoBehaviour
{

    public float maxHealth = 50f;
    public float currentHealth;
    public float damageMultiplier = 1f;

    public UIHealthBar healthBar;


    private void Awake()
    {

        currentHealth = maxHealth;
        enemyRigidbody = GetComponent<Rigidbody>();
    }
    public void TakeDamage(float amountDamage)
    {
        currentHealth -= amountDamage * damageMultiplier;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);


        if (currentHealth <= 0f)
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

    private bool isMarked = false;
    public void ApplyMark(float damageIncreasePercentage)
    {
        if (!isMarked)
        {
            // Augmenter le multiplicateur de d�g�ts de l'ennemi
            damageMultiplier = damageMultiplier * (1f + damageIncreasePercentage / 100f);


            isMarked = true;
        }
    }

    public void RemoveMark()
    {
        if (isMarked)
        {
            // R�tablir le multiplicateur de d�g�ts de base de l'ennemi
            damageMultiplier = 1f;

            isMarked = false;
        }
    }
    private bool isMovementDisabled = false;
    private bool isAttackDisabled = false;

    private Rigidbody enemyRigidbody; // R�f�rence au composant Rigidbody de l'ennemi




    public void DisableMovementAndAttack(float duration)
    {
        isMovementDisabled = true;
        isAttackDisabled = true;
        DisableMovement();

        StartCoroutine(EnableMovementAndAttackAfterDelay(duration));
    }

    private IEnumerator EnableMovementAndAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableMovement();
        isMovementDisabled = false;
        isAttackDisabled = false;
    }

    public void DisableMovement()
    {
        isMovementDisabled = true;
        enemyRigidbody.velocity = Vector3.zero; // Arr�ter le mouvement en r�initialisant la vitesse
        enemyRigidbody.isKinematic = true; // D�sactiver les effets de physique sur l'ennemi
    }

    public void EnableMovement()
    {
        isMovementDisabled = false;
        enemyRigidbody.isKinematic = false; // Activer � nouveau les effets de physique sur l'ennemi
    }
}
