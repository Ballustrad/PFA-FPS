using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TimerUi : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float levelDuration = 180f; // Dur�e du niveau en secondes (3 minutes)
    private float levelTimer = 0f; // Compteur de temps pour le niveau
    public bool levelIsStarted;
    
   
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

    private void EndLevel()
    {
        levelIsStarted = false;
        levelTimer = 0f;

        // Arr�ter le spawn des ennemis
        CancelInvoke("SpawnEnemy");

        // D�truire tous les ennemis pr�sents dans la sc�ne
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Afficher un �cran de fin de niveau ou effectuer d'autres actions sp�cifiques � la fin du niveau
        // ...

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
    }
}

