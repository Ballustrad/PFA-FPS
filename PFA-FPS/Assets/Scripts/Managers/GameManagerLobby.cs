using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GameManagerLobby : Singleton<GameManagerLobby>
{
    public GameObject normal;
    public GameObject fast;
    public GameObject heavy;
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    
    public Transform parent;
    public Transform currentPlayer;
    public GameObject player;

    public CurrentPlayerMiddleMan currentPlayerMiddleMan;
    private void Awake()
    {
        string selectedPlayer = PlayerPrefs.GetString("SelectedPlayer", "fast");
        GameObject player = Instantiate(GetPlayerPrefab(selectedPlayer), spawnPoint, Quaternion.identity, parent);
        currentPlayer = player.transform;
        
    }

    private GameObject GetPlayerPrefab(string playerName)
    {
        switch (playerName)
        {
            case "fast":
                return fast;
            case "normal":
                return normal;
            case "heavy":
                return heavy;
            default:
                return fast;
        }
    }
}
