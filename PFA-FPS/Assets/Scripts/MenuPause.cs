using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Met le jeu en pause en réglant l'échelle de temps à 0
        Cursor.lockState = CursorLockMode.None; // Débloque la souris
        Cursor.visible = true; // Rend le curseur visible
        pausePanel.SetActive(true); // Affiche le panneau de pause
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Rétablit l'échelle de temps normale
        Cursor.lockState = CursorLockMode.Locked; // Verrouille la souris
        Cursor.visible = false; // Rend le curseur invisible
        pausePanel.SetActive(false); // Masque le panneau de pause
    }

   public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    
}
