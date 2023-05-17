using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsNormal : MonoBehaviour
{
    #region
    public float paralysisDuration = 5f;
    public float explosionRadius = 5f;
    public float explosionDelay = 3f;
    public LayerMask enemyLayer;
    public GameObject explosionParticle;
    public AudioClip explosionSound;
    public LayerMask layerToHit;
    public float grenadeCooldown = 15f;
    private bool grenadeAvailable = true;
    #endregion

   

    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(grenadeCooldown);
        grenadeAvailable = true;
    }

    public void ThrowGrenade()
    {
        // Cr�er une instance de l'objet de grenade et la positionner � la position du joueur
        GameObject grenade = Instantiate(gameObject, transform.position, transform.rotation);
        // Ajouter une force � la grenade pour la lancer
        grenade.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);

        // Programmer l'explosion de la grenade apr�s un d�lai
        StartCoroutine(ExplodeGrenade(grenade));
    }

    // La fonction pour g�rer l'explosion de la grenade
    IEnumerator ExplodeGrenade(GameObject grenade)
    {
        // Attendre le d�lai d'explosion
        yield return new WaitForSeconds(explosionDelay);
        // Cr�er une sph�re d'impact autour de la grenade
        Collider[] colliders = Physics.OverlapSphere(grenade.transform.position, explosionRadius, enemyLayer);

        // Paralyser les ennemis touch�s par la grenade
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.layer == layerToHit)
            {
                hit.GetComponent<Target>().Paralyze(paralysisDuration);
            }
        }

        // Jouer la particule et le son d'explosion
        Instantiate(explosionParticle, grenade.transform.position, grenade.transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSound, grenade.transform.position);

        // D�truire la grenade
        Destroy(grenade);
    }


    public float duration = 5f;
    public float reductionAmount = 0.2f;
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
    }
}
