using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 10;

    private Vector3 direction;

    public PlayerMovement playerMovement;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        playerMovement = gameManager.currentPlayer.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("damage " + damage);
            playerMovement.TakeDamage(damage);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
