using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsNormal : MonoBehaviour
{
    #region
    public float paralysisDuration = 5f;
    public float explosionRadius = 10f;
    public float explosionDelay = 1f;
    public LayerMask enemyLayer;
    //public GameObject explosionParticle;
   // public AudioClip explosionSound;
    public LayerMask layerToHit;
    public float grenadeCooldown = 15f;
    private bool grenadeAvailable = true;
    public GameObject grenadePara;
    public GameObject spawnPointGrenade;
    #endregion

   

    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(grenadeCooldown);
        grenadeAvailable = true;
    }

    public void ThrowGrenade()
    {
      
        GameObject grenade = Instantiate(grenadePara, spawnPointGrenade.transform.position, transform.rotation);
        

        grenade.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);

    }
  

    // La fonction pour gérer l'explosion de la grenade
   


    public float duration = 5f;
    public float reductionAmount = 15f;
    public float cooldown = 15f;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

   

    private IEnumerator ApplyDamageReduction()
    {
        var player = GetComponent<PlayerMovement>(); // Replace Player avec le nom de ton script de joueur
        player.damageReduction += reductionAmount;
        yield return new WaitForSeconds(duration);
        player.damageReduction -= reductionAmount;
    }







    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isOnCooldown)
        {
            isOnCooldown = true;
            cooldownTimer = cooldown;
            StartCoroutine(ApplyDamageReduction());
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }


        if (Input.GetMouseButtonDown(1) && grenadeAvailable)
        {
            ThrowGrenade();
            grenadeAvailable = false;
            StartCoroutine(GrenadeCooldown());
        }
        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldownMark)
        {
            StartCoroutine(UseMarkAbility());
        }
    }





    public float damageIncreasePercentage = 20f;
    public float durationMark = 4f;
    public float cooldownMark = 10f;
    private bool isOnCooldownMark = false;

    

    private IEnumerator UseMarkAbility()
    {
        // Lancer la compétence
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Target enemy = hit.collider.GetComponent<Target>();
                if (enemy != null)
                {
                    // Appliquer la marque
                    enemy.ApplyMark(20);
                    yield return new WaitForSeconds(durationMark);
                    // Retirer la marque après la durée
                    enemy.RemoveMark();
                }
            }
        }

        // Démarrer le temps de recharge
        isOnCooldownMark = true;
        yield return new WaitForSeconds(cooldownMark);
        isOnCooldownMark = false;
    }
}

