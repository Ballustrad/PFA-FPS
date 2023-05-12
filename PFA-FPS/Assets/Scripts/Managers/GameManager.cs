using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject normal;
    public GameObject fast;
    public GameObject lourd;
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    public GameObject panelSelection;
    public Transform parent;

    public TextMeshProUGUI ammo;
    
    public void SelectCharacter(GameObject name)
    {
        Instantiate(name, spawnPoint , Quaternion.identity, parent);
        panelSelection.SetActive(false);
    }
}
