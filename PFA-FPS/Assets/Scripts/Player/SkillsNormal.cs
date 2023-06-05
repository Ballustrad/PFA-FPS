using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public  GameManager gameManager;
    public float duration = 5f;
    public float reductionAmount = 15f;
    public float cooldown = 15f;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;


    public Image skill1;
    public Image skill2;
    public Image skill3;
    private Color originalS1;
    private Color originalS2;
    private Color originalS3;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        originalS1 = skill1.GetComponent<Image>().color;
        originalS2 = skill2.GetComponent<Image>().color;
        originalS3 = skill3.GetComponent<Image>().color;
    }


    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(grenadeCooldown);
        grenadeAvailable = true;
        skill1.GetComponent<Image>().color = originalS1;
    }

    public void ThrowGrenade()
    {
      
        GameObject grenade = Instantiate(grenadePara, spawnPointGrenade.transform.position, transform.rotation);
        

        grenade.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);

    }
  

    // La fonction pour gérer l'explosion de la grenade
   



   

    private IEnumerator ApplyDamageReduction()
    {
        var player = GetComponent<PlayerMovement>(); // Replace Player avec le nom de ton script de joueur
        player.damageReduction += reductionAmount;
        yield return new WaitForSeconds(duration);
        player.damageReduction -= reductionAmount;
    }







    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gameManager.canUseSkill2 == true  && !isOnCooldown)
        {
            
            isOnCooldown = true;
            cooldownTimer = cooldown;
            StartCoroutine(ApplyDamageReduction());
            skill2.GetComponent<Image>().color = Color.red;
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                skill2.GetComponent<Image>().color = originalS2;
            }
        }


        if (Input.GetMouseButtonDown(1) && gameManager.canUseSkill1 == true && grenadeAvailable)
        {
            ThrowGrenade();
            grenadeAvailable = false;
            StartCoroutine(GrenadeCooldown());
            skill1.GetComponent<Image>().color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.E) && gameManager.canUseSkill3 == true && !isOnCooldownMark)
        {
            StartCoroutine(UseMarkAbility());
            skill3.GetComponent<Image>().color = Color.red;
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
        skill3.GetComponent<Image>().color = originalS3;
    }
}

