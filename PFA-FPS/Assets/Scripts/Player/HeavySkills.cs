using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySkills : MonoBehaviour
{
    public float buffDuration = 5f; // Durée de l'effet de buff en secondes
    public float buffCooldown = 20f; // Temps de recharge du buff en secondes

    private bool isBuffActive = false;
    private float buffTimer = 0f;
    private float buffCooldownTimer = 0f;

    private BaseWeapon baseWeapon;
    public GameManager gameManager;

    private void Awake()
    {
        baseWeapon = GetComponent<BaseWeapon>();
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (isBuffActive)
        {
            UpdateBuff();
        }
        else
        {
            if (buffCooldownTimer > 0)
            {
                buffCooldownTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(1) && gameManager.canUseSkill1 == true)
                {
                    StartBuff();
                }
            }
        }
    }

    private void StartBuff()
    {
        if (buffCooldownTimer > 0)
        {
            return; // Le buff est en cours de recharge
        }

        isBuffActive = true;
        buffTimer = buffDuration;
        buffCooldownTimer = buffCooldown;

        // Appliquer les modifications de la cadence de tir et de la vitesse de rechargement
        baseWeapon.ModifyFireRate(2f); // Exemple : Double la cadence de tir
        baseWeapon.ModifyReloadSpeed(0.5f); // Exemple : Réduit le temps de rechargement de moitié
    }

    private void UpdateBuff()
    {
        buffTimer -= Time.deltaTime;

        if (buffTimer <= 0f)
        {
            isBuffActive = false;

            // Réinitialiser les modifications de la cadence de tir et de la vitesse de rechargement
            baseWeapon.ResetFireRate();
            baseWeapon.ResetReloadSpeed();
        }
    }



}
