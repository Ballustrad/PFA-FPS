using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using JetBrains.Annotations;
using UnityEngine.UI;

public class TimerUi : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float levelDuration = 180f; // Dur�e du niveau en secondes (3 minutes)
    private float levelTimer = 0f; // Compteur de temps pour le niveau
    public bool levelIsStarted;
    public GameManager gameManager;
    public ScoreManager scoreManager;
    public MiddleManScenehandler middleManScenehandler;
    public GameObject chronotimer;
    [Space]
    public GameObject panelEnd;
    public TextMeshProUGUI currentLevel;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        scoreManager = ScoreManager.Instance;
        scoreManager.UpdateScoreTextRound();
    }
    private void Update()
    {
        levelTimer += Time.deltaTime;

        if (levelTimer >= levelDuration)
        {
            // Fin du niveau, effectuez les actions n�cessaires
            EndLevel();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        float remainingTime = levelDuration - levelTimer;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndLevel()
    {
        levelIsStarted = false;
        levelTimer = 0f;
        middleManScenehandler.indexLevel += 1;
        // Arr�ter le spawn des ennemis
        CancelInvoke("SpawnEnemy");

        // D�truire tous les ennemis pr�sents dans la sc�ne
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Afficher un �cran de fin de niveau ou effectuer d'autres actions sp�cifiques � la fin du niveau
        panelEnd.SetActive(true);
        currentLevel.text = "level" + middleManScenehandler.indexLevel.ToString() + "Completed";
        chronotimer.GetComponent<Image>().enabled = false;
        chronotimer.GetComponentInChildren<Text>().enabled = false;
        
        // ...
        middleManScenehandler.currentPoint += middleManScenehandler.pointOfRound;
        // Charger la sc�ne du lobby apr�s un d�lai de 5 secondes
        StartCoroutine(LoadLobbyScene());
    }

    private IEnumerator LoadLobbyScene()
    {
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene("Lobby");
    }

    public void StartLevel()
    {
        levelIsStarted = true;
        levelTimer = 0f;
        middleManScenehandler.pointOfRound = 0f;
        scoreManager.UpdateScoreTextRound();
    }
}

