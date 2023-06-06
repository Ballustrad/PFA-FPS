using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float explosionRadius = 8f; // Rayon de l'explosion
    public float damageAmount = 20f; // Montant des d�g�ts � infliger
    public GameObject missileExplosion;
    private void OnTriggerEnter(Collider other)
    {
        // V�rifier si le missile a collisionn� avec un ennemi
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            GameObject effect = Instantiate(missileExplosion, transform.position, Quaternion.identity, transform);
            // Obtenir tous les ennemis dans la zone d'explosion
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            // Infliger des d�g�ts � tous les ennemis dans la zone d'explosion
            foreach (Collider collider in colliders)
            {
                Target enemy = collider.GetComponent<Target>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
