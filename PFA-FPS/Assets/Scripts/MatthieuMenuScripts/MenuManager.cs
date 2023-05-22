using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pointerEnterSound;

    public GameObject InstructionsPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    public void PlayGame()
    {
        audioSource.PlayOneShot(pointerEnterSound);
        Debug.Log("Play!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenInstructionsPanel()
    {
        audioSource.PlayOneShot(pointerEnterSound);
        CloseAllPanels();
        InstructionsPanel.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        audioSource.PlayOneShot(pointerEnterSound);
        CloseAllPanels();
        SettingsPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        audioSource.PlayOneShot(pointerEnterSound);
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
        audioSource.PlayOneShot(pointerEnterSound);
        Debug.Log("Quit");
        Application.Quit();
    }
}
