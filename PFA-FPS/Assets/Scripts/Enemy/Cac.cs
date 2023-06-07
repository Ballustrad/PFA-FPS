using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cac : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public int meleeDamage = 25;
    public float attackInterval = 1.0f;

    private bool canAttack = true;
    public PlayerMovement playerMovement;
    public GameManager gameManager;

    public Animator animator;

    public void Awake()
    {
        gameManager = GameManager.Instance;
      
        if (gameManager.currentPlayer != null)
        {
            playerMovement = gameManager.currentPlayer.GetComponent<PlayerMovement>();
        }
        else
        {
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collisionYEs");
        if (canAttack && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playerhit");
            animator.SetBool("isAttacking",true);
            playerMovement.TakeDamage(meleeDamage);
            Debug.Log("hit is" + other.gameObject.name);
            canAttack = false;
            Invoke("ResetAttack", attackInterval);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
