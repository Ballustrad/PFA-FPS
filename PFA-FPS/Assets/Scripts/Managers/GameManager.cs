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
    public Transform currentPlayer;
    public GameObject player;
    

    public TextMeshProUGUI ammo;


    public void SelectCharacter(GameObject name)
    {
        GameObject player = Instantiate(name, spawnPoint, Quaternion.identity, parent);
        panelSelection.SetActive(false);
        currentPlayer = player.transform;
        
    }

    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    public float spawnInterval = 5f;
    public Transform enemyParent;
    private float lastSpawnTime = 0f;

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, enemyParent);
            lastSpawnTime = Time.time;
        }
        
    }
    
}
