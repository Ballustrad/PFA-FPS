using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;
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
    public MiddleManScenehandler middleManScenehandler;
    public GameObject mainCamera;

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
        if (middleManScenehandler.level4IsHere == true )
        {
            ChangeToSolidColorBackground(Color.black);
        }
        else
        {
            RestorePreviousBackground();
        }
    }

    public void ChangeToSolidColorBackground(Color color)
    {
        // Créez un nouveau matériau avec une couleur solide
        Material solidColorMaterial = new Material(Shader.Find("Unlit/Color"));
        solidColorMaterial.color = color;

        // Définir le mode de rendu de la caméra sur "Custom" pour utiliser le nouveau matériau
        mainCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
        mainCamera.GetComponent<Camera>().backgroundColor = Color.black;
        mainCamera.GetComponent<Camera>().allowHDR = false;
        mainCamera.GetComponent<Camera>().allowMSAA = false;
        mainCamera.GetComponent<Camera>().allowDynamicResolution = false;
        mainCamera.GetComponent<Camera>().opaqueSortMode = OpaqueSortMode.Default;
        mainCamera.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
        mainCamera.GetComponent<Camera>().targetTexture = null;
        mainCamera.GetComponent<Camera>().SetReplacementShader(Shader.Find("Unlit/Color"), "");

        // Affecter le nouveau matériau à la caméra
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
        mainCamera.GetComponent<Camera>().SetReplacementShader(solidColorMaterial.shader, solidColorMaterial.shader.name);
    }
    public void RestorePreviousBackground()
    {
        // Réinitialiser les paramètres de la caméra pour restaurer le fond précédent
        mainCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        mainCamera.GetComponent<Camera>().ResetReplacementShader();
        mainCamera.GetComponent<Camera>().ResetAspect();
        mainCamera.GetComponent<Camera>().ResetCullingMatrix();
        mainCamera.GetComponent<Camera>().ResetProjectionMatrix();
        mainCamera.GetComponent<Camera>().ResetReplacementShader();
        mainCamera.GetComponent<Camera>().ResetReplacementShader();
        mainCamera.GetComponent<Camera>().ResetReplacementShader();
        mainCamera.GetComponent<Camera>().ResetReplacementShader();
    }


    public void Healing(float amount)
    {
        currentHealth += amount;
        

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
