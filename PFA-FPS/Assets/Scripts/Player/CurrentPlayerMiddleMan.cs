using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CurrentPlayer", menuName = "Data/CurrentPlayerData")]
public class CurrentPlayerMiddleMan : ScriptableObject
{
    public GameObject fast;
    public GameObject normal;
    public GameObject heavy;

    [SerializeField]
    public static GameObject currentPlayerInstance;
   public void SetCurrentPlayerTo(String name)
    {
        switch (name)
        {
            case "fast":
                currentPlayerInstance = fast;
                break;
            case "normal":
                currentPlayerInstance = normal;
                break;
            case "heavy":
                currentPlayerInstance = heavy;
                break;

            default:
                break;

        }
        
    }
    
}
