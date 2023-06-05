using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsFast : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public float healingPercentage = 50f;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    private Color originalS1;
    private Color originalS2;
    private Color originalS3;
    private bool isOnCooldownHeal = false;
    private float cooldownHeal = 10f;
    public GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.Instance;

        originalS1 = skill1.GetComponent<Image>().color;
        originalS2 = skill2.GetComponent<Image>().color;
        originalS3 = skill3.GetComponent<Image>().color;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameManager.canUseSkill1 == true && !isOnCooldownHeal)
        {
            UseHealingAbility();
            skill1.GetComponent<Image>().color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.A) && gameManager.canUseSkill2 == true && !isOnCooldownFreeze)
        {
            UseDisableEnemiesAbility();
            skill2.GetComponent<Image>().color = Color.red;
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
                skill3.GetComponent<Image>().color = originalS3;
                if (Input.GetKeyDown(KeyCode.E) && gameManager.canUseSkill3 == true)
                {
                    StartDash();
                    skill3.GetComponent<Image>().color = Color.red;
                }
            }
        }
    }

    private void UseHealingAbility()
    {

        playerMovement.Healing(healingPercentage);

        // D�marrer le temps de recharge
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
        skill1.GetComponent<Image>().color = originalS1;
    }



    public float disableDuration = 4f;
    public float cooldownFreeze = 20f;
    public LayerMask enemyLayer;

    private bool isOnCooldownFreeze = false;

    

    private void UseDisableEnemiesAbility()
    {
        // D�sactiver les ennemis dans le champ de vision de la cam�ra du joueur
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<Camera>().fieldOfView, enemyLayer);
        foreach (Collider collider in colliders)
        {
            Target enemy = collider.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.Paralyze(disableDuration);
            }
        }

        // D�marrer le temps de recharge
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
        skill2.GetComponent<Image>().color = originalS2;

    }

    public float dashDistance = 50f; // Distance du dash
    public float dashDuration = 1f; // Dur�e du dash
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
        Vector3 dashDirection = transform.forward; // Direction du dash (peut �tre modifi�e selon vos besoins)
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
