using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiddleManScenehandler", menuName = "Data/MiddleManScenehandler")]
public class MiddleManScenehandler : ScriptableObject
{
    [SerializeField]
    public static GameObject MiddleManSceneInstance;
    public float indexLevel = 0;
    public  bool levelIsStarted;
    public  float pointOfRound;
    public   float currentPoint;
   
    
}
