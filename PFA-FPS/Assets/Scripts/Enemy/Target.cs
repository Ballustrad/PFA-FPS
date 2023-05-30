
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.AI;

public class Target : MonoBehaviour
{

    public float maxHealth = 50f;
    public float currentHealth;
    public float damageMultiplier = 1f;
    public GameObject deathExplosion;
    public float valuePoints;
   
    public UIHealthBar healthBar;
    public GameManager gameManager;
    public ScoreManager scoreManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        scoreManager = ScoreManager.Instance;
        currentHealth = maxHealth;
        enemyRigidbody = GetComponent<Rigidbody>();
    }
    public void TakeDamage(float amountDamage)
    {
        currentHealth -= amountDamage + damageMultiplier;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);

        // Affiche les d�g�ts � c�t� de l'impact
        ShowDamageText(amountDamage);

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
            GetComponent<NavMeshAgent>().speed = 0;



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
        GetComponent<NavMeshAgent>().speed = 7;

        // R�initialise la variable isParalyzed
        isParalyzed = false;
    }
    public void Die()
    {
        if (gameManager.healthDecay == true)
        {
            gameManager.player.GetComponent<PlayerMovement>().Healing(20);
        }
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        gameManager.EnemyDeathSound();
        scoreManager.AddPoints(valuePoints);
        Destroy(gameObject);
    }

    private bool isMarked = false;
    public void ApplyMark(float damageIncreasePercentage)
    {
        if (!isMarked)
        {
            // Augmenter le multiplicateur de d�g�ts de l'ennemi
            damageMultiplier = damageIncreasePercentage;


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

    public GameObject damageTextPrefab; // R�f�rence au pr�fab d'affichage des d�g�ts

    public void ShowDamageText(float damage)
    {
        // Instancie le pr�fab d'affichage des d�g�ts � l'endroit de l'impact
       // GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);

        // Obtient le composant de texte (ou autre �l�ment visuel) du pr�fab
       // TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();

        // D�finit le texte des d�g�ts � afficher
       // damageText.text = damage.ToString();

        // D�truit le pr�fab d'affichage des d�g�ts apr�s un certain d�lai (par exemple, 1 seconde)
        //Destroy(damageTextObject, 1f);
    }


    
    public bool IsParalyzed
    {
        get { return isParalyzed; }
    }
}
