using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavySkills : MonoBehaviour
{
    public float buffDuration = 5f; // Durée de l'effet de buff en secondes
    public float buffCooldown = 20f; // Temps de recharge du buff en secondes

    private bool isBuffActive = false;
    private float buffTimer = 0f;
    private float buffCooldownTimer = 0f;

    public BaseWeapon baseWeapon;
    public GameManager gameManager;
    [Space]
    public float skillStompRadius = 5f; // Rayon de la zone d'effet
    public float stompStunDuration = 2f; // Durée de la paralysie en secondes
    public float stompDamageAmount = 10f; // Montant des dégâts à infliger
    public bool stompAvailable = true;
    public float stompCooldown = 5f;
    public GameObject stompEffect;
    public Transform stompEffectTransform;
    [Space]
    public LayerMask enemyLayer;
    [Space]
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    private Color originalS1;
    private Color originalS2;
    private Color originalS3;
    public GameObject skill1Locked;
    public GameObject skill2Locked;
    public GameObject skill3Locked;

    [Space]
    public GameObject missilePrefab; // Préfabriqué du missile
    public Transform launchPoint; // Point de lancement du missile
    public float missileSpeed = 10f; // Vitesse du missile
    
    private bool missileAvailable = true; // Indicateur de disponibilité de la compétence
    private float missileCooldownDuration = 5f; // Durée du temps de recharge (cooldown)


    

    private void Awake()
    {
        
        gameManager = GameManager.Instance;
        originalS1 = skill1.GetComponent<Image>().color;
        originalS2 = skill2.GetComponent<Image>().color;
        originalS3 = skill3.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (gameManager.canUseSkill1 == true)
        {
            skill1Locked.SetActive(false);
            skill1.SetActive(true);
        }
        if (gameManager.canUseSkill2 == true)
        {
            skill2Locked.SetActive(false);
            skill2.SetActive(true);
        }
        if (gameManager.canUseSkill3 == true)
        {
            skill3Locked.SetActive(false);
            skill3.SetActive(true);
        }
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
                skill1.GetComponent<Image>().color = originalS1;
                if (Input.GetMouseButton(1) && gameManager.canUseSkill1 == true)
                {
                    StartBuff();
                    skill1.GetComponent<Image>().color = Color.red;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && gameManager.canUseSkill2 == true && stompAvailable == true)
        {
            Stomp();
        }
        if (Input.GetKeyDown(KeyCode.E) && gameManager.canUseSkill3 == true && missileAvailable == true)
        {
            Missile();
        }

    }
    public void Stomp()
    {
        if (stompAvailable == true)
        {
            if (stompEffect != null)
            {
                GameObject effect = Instantiate(stompEffect, stompEffectTransform.position, Quaternion.identity, transform);
                Destroy(effect, 3f); // Détruit l'effet de guérison après une seconde
            }
            // Obtenir tous les ennemis dans la zone d'effet
            Collider[] colliders = Physics.OverlapSphere(transform.position, skillStompRadius, enemyLayer);

            // Paralyser les ennemis et leur infliger des dégâts
            foreach (Collider collider in colliders)
            {
                Target enemy = collider.GetComponent<Target>();
                if (enemy != null)
                {
                    enemy.Paralyze(stompStunDuration);
                    enemy.TakeDamage(stompDamageAmount);
                }
            }
            StartCoroutine(StartCooldownStomp());
        }
    }

    private IEnumerator StartCooldownStomp()
    {
        // Désactiver la disponibilité de la compétence
        stompAvailable = false;
        skill2.GetComponent<Image>().color = Color.red;
        // Attendre la durée du temps de recharge (cooldown)
        yield return new WaitForSeconds(stompCooldown);

        // Réactiver la disponibilité de la compétence
        stompAvailable = true;
        skill2.GetComponent<Image>().color = originalS2;
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
        baseWeapon.ModifyFireRate(5f); // Exemple : Double la cadence de tir
        baseWeapon.ModifyReloadSpeed(2f); // Exemple : Réduit le temps de rechargement de moitié
    }

    private void UpdateBuff()
    {
        buffTimer -= Time.deltaTime;

        if (buffTimer <= 0f)
        {
            isBuffActive = false;

            // Réinitialiser les modifications de la cadence de tir et de la vitesse de rechargement
            baseWeapon.ResetFireRate(5f);
            baseWeapon.ResetReloadSpeed(2f);
        }
    }
    public void Missile()
    {
        // Vérifier si la compétence est disponible
        if(missileAvailable == true)
        {
            // Instancier le missile et le lancer
            GameObject missile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
            Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();
            missileRigidbody.velocity = launchPoint.forward * missileSpeed;

            // Détruire le missile après un délai
            Destroy(missile, 5f);

            // Démarrer le temps de recharge (cooldown)
            StartCoroutine(StartCooldownMissile());

        }

    }

    private IEnumerator StartCooldownMissile()
    {
        // Désactiver la disponibilité de la compétence
        missileAvailable = false;
        skill3.GetComponent<Image>().color = Color.red;
        // Attendre la durée du temps de recharge (cooldown)
        yield return new WaitForSeconds(missileCooldownDuration);

        // Réactiver la disponibilité de la compétence
        missileAvailable = true;
        skill3.GetComponent<Image>().color = originalS3;
    }

}
