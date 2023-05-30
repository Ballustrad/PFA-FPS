using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsFast : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public float healingPercentage = 0.35f;

    private bool isOnCooldownHeal = false;
    private float cooldownHeal = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isOnCooldownHeal)
        {
            UseHealingAbility();
        }
        if (Input.GetKeyDown(KeyCode.A) && !isOnCooldownFreeze)
        {
            UseDisableEnemiesAbility();
        }

        if (isDashing)
        {
            Dash();
        }
        else
        {
            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartDash();
                }
            }
        }
    }

    private void UseHealingAbility()
    {

        playerMovement.Healing(healingPercentage);

        // Démarrer le temps de recharge
        StartCooldownHeal();
    }

    private void StartCooldownHeal()
    {
        isOnCooldownHeal = true;
        Invoke(nameof(ResetCooldownHeal), cooldownHeal);
    }

    private void ResetCooldownHeal()
    {
        isOnCooldownHeal = false;
    }



    public float disableDuration = 4f;
    public float cooldownFreeze = 20f;
    public LayerMask enemyLayer;

    private bool isOnCooldownFreeze = false;

    

    private void UseDisableEnemiesAbility()
    {
        // Désactiver les ennemis dans le champ de vision de la caméra du joueur
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<Camera>().fieldOfView, enemyLayer);
        foreach (Collider collider in colliders)
        {
            Target enemy = collider.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.Paralyze(disableDuration);
            }
        }

        // Démarrer le temps de recharge
        StartCooldown();
    }

    private void StartCooldown()
    {
        isOnCooldownFreeze = true;
        Invoke(nameof(ResetCooldown), cooldownFreeze);
    }

    private void ResetCooldown()
    {
        isOnCooldownFreeze = false;
    }

    public float dashDistance = 50f; // Distance du dash
    public float dashDuration = 1f; // Durée du dash
    public float dashCooldown = 5f; // Temps de recharge du dash

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashStartPosition;
    public GameObject dashPrefab;
    public CharacterController controller;


    private GameObject dashVFX;

    private void StartDash()
    {
        if (dashCooldownTimer > 0)
        {
            return; // Le dash est en cours de recharge
        }

        isDashing = true;
        dashTimer = dashDuration;
        dashStartPosition = transform.position;
        dashCooldownTimer = dashCooldown;

        // Appliquer une force pour effectuer le dash
        Vector3 dashDirection = transform.forward; // Direction du dash (peut être modifiée selon vos besoins)
        controller.Move(dashDirection * dashDistance);

        dashVFX = Instantiate(dashPrefab, transform.position, transform.rotation);
    }

    private void Dash()
    {
        dashTimer -= Time.deltaTime;
        Instantiate(dashPrefab);
        if (dashTimer <= 0f)
        {
            isDashing = false;
            if (dashVFX != null)
            {
                Destroy(dashVFX);
            }
        }
    }
}
