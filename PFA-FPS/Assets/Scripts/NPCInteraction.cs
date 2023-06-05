using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject panel;
    public bool isInteractable = true;

    private bool isPanelOpen = false;
    private bool isGamePaused = false;
    private bool isMouseLocked = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isInteractable)
        {
            ShowInteractionPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInteractable)
        {
            ShowInteractionPrompt(false);
            ClosePanel();
        }
    }

    private void Update()
    {
        if (isPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                TogglePanel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (isInteractable)
                {
                    OpenPanel();
                    PauseGame(true);
                }
            }
        }
    }

    private void ShowInteractionPrompt(bool show)
    {
        // Afficher ou masquer le prompt d'interaction
        // (par exemple, un message "Appuyez sur X pour interagir")
    }

    private void OpenPanel()
    {
        panel.SetActive(true);
        isPanelOpen = true;
        LockMouseCursor(false);
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
        isPanelOpen = false;
        LockMouseCursor(true);
    }

    private void TogglePanel()
    {
        if (isPanelOpen)
        {
            ClosePanel();
            PauseGame(false);
        }
        else
        {
            OpenPanel();
            PauseGame(true);
        }
    }

    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
        isGamePaused = pause;
    }

    private void LockMouseCursor(bool lockCursor)
    {
        isMouseLocked = lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }
}
