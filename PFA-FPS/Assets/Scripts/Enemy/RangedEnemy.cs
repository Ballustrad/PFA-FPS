using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingDistance = 10.0f;
    public float shootingCooldown = 2.0f;

    private bool canShoot = true;
    private NavMeshAgent agent;
    private Transform playerTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameManager.Instance.currentPlayer;
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= shootingDistance)
        {
            agent.isStopped = true; // Arrête le déplacement via NavMeshAgent

            // Rotate towards the player
            Vector3 targetDirection = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

            if (canShoot)
            {
                Shoot();
                canShoot = false;
                Invoke("ResetShootingCooldown", shootingCooldown);
            }
        }
        else
        {
            agent.isStopped = false; // Reprend le déplacement via NavMeshAgent
            agent.SetDestination(playerTransform.position); // Définit la destination du NavMeshAgent vers le joueur
        }
    }
    public AudioSource shootSource;
    public AudioClip shootClip;
    private void Shoot()
    {
        // Instantiate the projectile and set its direction towards the player
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        projectile.GetComponent<EnemyProjectile>().SetDirection(direction);
        if (shootClip != null && shootSource != null)
        {
            shootSource.PlayOneShot(shootClip);
        }
    }

    private void ResetShootingCooldown()
    {
        canShoot = true;
    }
}
