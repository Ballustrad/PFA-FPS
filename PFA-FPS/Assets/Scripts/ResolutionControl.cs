using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    public void OnResolutionChanged()
    {
        // Récupérer la résolution sélectionnée dans le menu déroulant
        string selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;

        // Diviser la chaîne de résolution en largeur et hauteur
        string[] resolutionValues = selectedResolution.Split('x');
        int width = int.Parse(resolutionValues[0]);
        int height = int.Parse(resolutionValues[1]);

        // Changer la résolution de l'écran
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
