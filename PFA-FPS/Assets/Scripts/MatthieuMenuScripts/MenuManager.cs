using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject InstructionsPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    public void PlayGame()
    {
        Debug.Log("Play!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenInstructionsPanel()
    {
        CloseAllPanels();
        InstructionsPanel.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        CloseAllPanels();
        SettingsPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        CloseAllPanels();
        CreditsPanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        InstructionsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
