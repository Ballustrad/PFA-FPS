using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject normal;
    public GameObject fast;
    public GameObject heavy;
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    


    public Transform parent;
    public Transform currentPlayer;
    public GameObject player;

    public CurrentPlayerMiddleMan currentPlayerMiddleMan;

    /*public GameObject uiIconSkills;
    public Image iconSkillLeftClick;
    public Image iconSkillRightClick;
    public Image iconSkillA;
    public Image iconSkillE;*/
    public TextMeshProUGUI ammo;
    public AudioClip deathSound; // Son à jouer à la mort de l'ennemi
    public AudioSource audioSource;
    public MiddleManScenehandler middleManScenehandler;

    
    
    public TimerUi timerUi;

    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
     
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        

        
        if (timerUi != null)
        {

            timerUi.StartLevel();
            
        }
        

        string selectedPlayer = PlayerPrefs.GetString("SelectedPlayer", "fast");
        GameObject newplayer = Instantiate(GetPlayerPrefab(selectedPlayer), spawnPoint, Quaternion.identity, parent);
        player = newplayer;
        currentPlayer = newplayer.transform;
        //uiIconSkills.SetActive(true);
    }
   
    private GameObject GetPlayerPrefab(string playerName)
    {
        switch (playerName)
        {
            case "fast":
                return fast;
            case "normal":
                return normal;
            case "heavy":
                return heavy;
            default:
                return fast;
        }
    }
    
    public void EnemyDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    
    

    public GameObject[] enemyPrefabs; // Tableau des prefabs des ennemis disponibles
    public Transform[] spawnPoints; // Tableau des emplacements de spawn
    public float spawnInterval = 5.0f; // Intervalle entre chaque spawn
    public float initialDelay = 2.0f; // Délai initial avant le premier spawn

    private void Start()
    {
        if (enemyPrefabs != null && spawnPoints != null && enemyPrefabs.Length > 0 && spawnPoints.Length > 0)
        {
            InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in GameManager.");
            return;
        }

        // Sélectionne un emplacement de spawn aléatoire
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Sélectionne un type d'ennemi aléatoire
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instancie l'ennemi au point de spawn sélectionné
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
