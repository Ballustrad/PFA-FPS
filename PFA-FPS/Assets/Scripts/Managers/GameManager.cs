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

    public GameObject uiIconSkills;
    public Image iconSkillLeftClick;
    public Image iconSkillRightClick;
    public Image iconSkillA;
    public Image iconSkillE;
    public TextMeshProUGUI ammo;
    public AudioClip deathSound; // Son à jouer à la mort de l'ennemi
    public AudioSource audioSource;
    private float levelDuration = 180f; // Durée du niveau en secondes (3 minutes)
    private bool isLevelActive = false; // Indique si le niveau est actif
    private float levelTimer = 0f; // Compteur de temps pour le niveau
    public TextMeshProUGUI timerText;
    public MiddleManScenehandler middleManScenehandler;

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

        string selectedPlayer = PlayerPrefs.GetString("SelectedPlayer", "fast");
        GameObject newplayer = Instantiate(GetPlayerPrefab(selectedPlayer), spawnPoint, Quaternion.identity, parent);
        player = newplayer;
        currentPlayer = newplayer.transform;
        uiIconSkills.SetActive(true);
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

    public void Update()
    {
        if (middleManScenehandler.levelIsStarted == true)
        {
            levelTimer += Time.deltaTime;

            if (levelTimer >= levelDuration)
            {
                EndLevel();
                middleManScenehandler.levelIsStarted = false;
            }

            UpdateTimerUI();
        }
    }

    public void StartLevel()
    {
        isLevelActive = true;
        levelTimer = 0f;
    }

    private void EndLevel()
    {
        isLevelActive = false;
        levelTimer = 0f;

        // Arrêter le spawn des ennemis
        CancelInvoke("SpawnEnemy");

        // Détruire tous les ennemis présents dans la scène
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Charger la scène du lobby après un délai de 5 secondes
        StartCoroutine(LoadLobbyScene());
    }

    private IEnumerator LoadLobbyScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Lobby");
    }

    private void UpdateTimerUI()
    {
        float remainingTime = levelDuration - levelTimer;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
