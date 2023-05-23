using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cac : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public int meleeDamage = 10;
    public float attackInterval = 1.0f;

    private bool canAttack = true;
    public PlayerMovement playerMovement;
    public GameManager gameManager;
    
    public void Awake()
    {
        gameManager = GameManager.Instance;
        playerMovement = gameManager.currentPlayer.GetComponent<PlayerMovement>();
    }
    

    private void OnCollisionStay(Collision collision)
    {
        if (canAttack && collision.gameObject.CompareTag("Player"))
        {
            playerMovement.TakeDamage(meleeDamage);

            canAttack = false;
            Invoke("ResetAttack", attackInterval);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
