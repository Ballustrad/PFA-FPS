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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenOptionsPanel()
    {
        InstructionsPanel.SetActive(true);
        CloseAllPanels();
    }

    public void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        CloseAllPanels();
    }

    public void OpenCreditsPanel()
    {
        CreditsPanel.SetActive(true);
        CloseAllPanels();
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
