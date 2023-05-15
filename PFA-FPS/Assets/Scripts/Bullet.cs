using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactEffect;
    private float effectDuration = 2f;

    public void Start()
    {
        
        transform.rotation = transform.rotation * Quaternion.Euler(90f, 90f, 90f);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.transform.GetComponent<Target>();
        // Instantiate l'effet d'impact à la position de la collision
        GameObject impact = Instantiate(impactEffect, collision.contacts[0].point, Quaternion.identity);
        // Détruit l'effet après un certain temps
        Destroy(impact, effectDuration);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print(collision.gameObject.name);
            target.TakeDamage(20);
            

        }
        Destroy(gameObject);
    }
}
