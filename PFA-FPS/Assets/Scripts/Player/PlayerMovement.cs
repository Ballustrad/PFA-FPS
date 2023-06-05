using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    public CharacterData characterData;
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float currentHealth;
    public float maxHealth;
    public float damageReduction = 0;
    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public HealthBar hpBar;

    [SerializeField] private Vector3 velocity;
    [SerializeField] public bool isGrounded;
   
    private void Start()
    {
         gameManager = GameManager.Instance;
        speed = characterData.speed;
        jumpHeight = characterData.jumpHeight;
        maxHealth = characterData.health;
        currentHealth = maxHealth;
       
        hpBar.SetState(currentHealth, maxHealth);
    }
    
    public GameObject healEffect;


    public void Healing(float amount)
    {
        currentHealth += amount;
        if (healEffect != null)
        {
            GameObject effect = Instantiate(healEffect, transform.position, Quaternion.identity, transform);
            Destroy(effect, 2f); // D�truit l'effet de gu�rison apr�s une seconde
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        hpBar.SetState(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        damage = damage - damageReduction;
        currentHealth = currentHealth - damage;
        hpBar.SetState(currentHealth, maxHealth);
        if (currentHealth < 0 )
        {
            Die();
        }
  
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    
}
