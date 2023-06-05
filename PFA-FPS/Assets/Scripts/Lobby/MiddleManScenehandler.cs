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
    public  float currentPoint;
    public bool canUseSkill1 = false;
    public bool canUseSkill2 = false;
    public bool canUseSkill3 = false;
    public bool level4IsHere = false;


}
