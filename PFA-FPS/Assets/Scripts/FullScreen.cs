using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Toggle fullscreenToggle;

    public void OnDisplayModeChanged()
    {
        // Récupérer l'état de la case à cocher
        bool isFullscreen = fullscreenToggle.isOn;

        // Changer le mode d'affichage
        Screen.fullScreen = isFullscreen;
    }
}
