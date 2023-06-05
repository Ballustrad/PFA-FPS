using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    public int explosionDamage = 50;
    public float explosionTimer = 2.0f;
    public PlayerMovement playerMovement;
    public GameManager gameManager;
    private bool isExploding = false;

    public void Awake()
    {
        gameManager = GameManager.Instance;

        if (gameManager.currentPlayer != null)
        {
            playerMovement = gameManager.currentPlayer.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("currentPlayer is null in GameManager.");
        }
    }


    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        
        if (!isExploding && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("boum");
            isExploding = true;
            Invoke("Explode", explosionTimer);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerMovement.TakeDamage(explosionDamage); 
            }
        }

        // Destroy the Kamikaze enemy
        Destroy(gameObject);
    }
}
