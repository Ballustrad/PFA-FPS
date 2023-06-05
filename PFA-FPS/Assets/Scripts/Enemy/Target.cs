
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

        // Affiche les dégâts à côté de l'impact
        ShowDamageText(amountDamage);

        if (currentHealth <= 0f)
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
            // Augmenter le multiplicateur de dégâts de l'ennemi
            damageMultiplier = damageIncreasePercentage;


            isMarked = true;
        }
    }

    public void RemoveMark()
    {
        if (isMarked)
        {
            // Rétablir le multiplicateur de dégâts de base de l'ennemi
            damageMultiplier = 1f;

            isMarked = false;
        }
    }
    private bool isMovementDisabled = false;
    private bool isAttackDisabled = false;

    private Rigidbody enemyRigidbody; // Référence au composant Rigidbody de l'ennemi

    public GameObject damageTextPrefab; // Référence au préfab d'affichage des dégâts

    public void ShowDamageText(float damage)
    {
        // Instancie le préfab d'affichage des dégâts à l'endroit de l'impact
       // GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);

        // Obtient le composant de texte (ou autre élément visuel) du préfab
       // TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();

        // Définit le texte des dégâts à afficher
       // damageText.text = damage.ToString();

        // Détruit le préfab d'affichage des dégâts après un certain délai (par exemple, 1 seconde)
        //Destroy(damageTextObject, 1f);
    }


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
        enemyRigidbody.velocity = Vector3.zero; // Arrêter le mouvement en réinitialisant la vitesse
        enemyRigidbody.isKinematic = true; // Désactiver les effets de physique sur l'ennemi
    }

    public void EnableMovement()
    {
        isMovementDisabled = false;
        enemyRigidbody.isKinematic = false; // Activer à nouveau les effets de physique sur l'ennemi
    }
    public bool IsParalyzed
    {
        get { return isParalyzed; }
    }
}
