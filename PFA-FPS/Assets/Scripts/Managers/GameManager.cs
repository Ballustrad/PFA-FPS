using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject normal;
    public GameObject fast;
    public GameObject lourd;
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    public GameObject panelSelection;
    public Transform parent;
    public Transform currentPlayer;
    public GameObject player;
    public Image backgroundImageHealth;
    public Image foreGroundImageHealth;
    public GameObject uiHealthBar;
    public GameObject uiIconSkills;
    public Image iconSkillLeftClick;
    public Image iconSkillRightClick;
    public Image iconSkillA;
    public Image iconSkillE;
    public TextMeshProUGUI ammo;
    private float parentWidth;
    private float width;
    public Slider slider;
    

   /*public void SetHealthBarPercentagePlayer(float percentage)
    {
    
        width = parentWidth * (1 / percentage);
      
        foreGroundImageHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }*/
    public void SelectCharacter(GameObject name)
    {
        GameObject player = Instantiate(name, spawnPoint, Quaternion.identity, parent);
        panelSelection.SetActive(false);
        currentPlayer = player.transform;
       // uiHealthBar.SetActive(true);
        uiIconSkills.SetActive(true);
        
    }

   
    


    public GameObject[] enemyPrefabs; // Tableau des prefabs des ennemis disponibles
    public Transform[] spawnPoints; // Tableau des emplacements de spawn
    public float spawnInterval = 5.0f; // Intervalle entre chaque spawn
    public float initialDelay = 2.0f; // Délai initial avant le premier spawn

    private void Start()
    {
        parentWidth = uiHealthBar.GetComponent<RectTransform>().rect.width;
        InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Sélectionne un emplacement de spawn aléatoire
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Sélectionne un type d'ennemi aléatoire
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instancie l'ennemi au point de spawn sélectionné
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

  
    
}
