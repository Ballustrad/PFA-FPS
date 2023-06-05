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

    
    public TextMeshProUGUI ammo;
    public AudioClip deathSound; // Son à jouer à la mort de l'ennemi
    public AudioSource audioSource;
    public MiddleManScenehandler middleManScenehandler;
    public bool healthDecay = false;
    public bool canUseSkill1;
    public bool canUseSkill2 ;
    public bool canUseSkill3 ;


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
        canUseSkill1 = middleManScenehandler.canUseSkill1;
        canUseSkill2 = middleManScenehandler.canUseSkill2;
        canUseSkill3 = middleManScenehandler.canUseSkill3;
    }

    public GameObject GetPlayerPrefab(string playerName)
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

    public float interval = 1f; // Intervalle de temps en secondes
    private float timer = 0f; // Compteur de temps

    private void Update()
    {
        Debug.Log("s1" + canUseSkill1);
        Debug.Log("s2" + canUseSkill2);
        Debug.Log("s3" + canUseSkill3);
        if (healthDecay == true)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                // Déclencher l'action
                PerformAction();
                timer = 0f;
            }
        }
        
    }

    private void PerformAction()
    {
        // Code pour l'action à effectuer chaque seconde
        player.GetComponent<PlayerMovement>().TakeDamage(5);
        // Ajoutez ici le code de l'action que vous souhaitez exécuter chaque seconde
    }



}
