using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // V�rifie si la collision se produit avec un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Appelle la fonction ExplodeGrenade du script SkillsNormal
            SkillsNormal skillsNormal = GetComponentInParent<SkillsNormal>();
            if (skillsNormal != null)
            {
                skillsNormal.ExplodeGrenade(gameObject);
            }
        }

        // D�truit la balle apr�s la collision
        Destroy(gameObject);
    }
}
