using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float explosionRadius = 8f; // Rayon de l'explosion
    public float damageAmount = 20f; // Montant des dégâts à infliger
    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si le missile a collisionné avec un ennemi
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            // Obtenir tous les ennemis dans la zone d'explosion
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            // Infliger des dégâts à tous les ennemis dans la zone d'explosion
            foreach (Collider collider in colliders)
            {
                Target enemy = collider.GetComponent<Target>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }
}
