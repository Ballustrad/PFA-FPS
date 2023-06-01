using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public float paralysisDuration = 5f;
    public float explosionRadius = 10f;
    public float explosionDelay = 1f;
    public LayerMask enemyLayer;
    public LayerMask layerToHit;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Appelle la fonction ExplodeGrenade du script SkillsNormal

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
            
            
            // Paralyser les ennemis touchés par la grenade
            foreach (Collider hit in colliders)
            {
                

                if (hit.gameObject.CompareTag("Enemy"))
                {
                    
                    hit.GetComponent<Target>().Paralyze(paralysisDuration);
                }
            }
            
        }

        // Détruit la balle après la collision
        Destroy(gameObject);
    }
    
}
