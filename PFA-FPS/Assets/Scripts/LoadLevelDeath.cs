using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelDeath : MonoBehaviour
{
    public void Laod()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; // Verrouille la souris
        Cursor.visible = false; // Rend le curseur invisible
        SceneManager.LoadScene("Lobby");
    }
}
