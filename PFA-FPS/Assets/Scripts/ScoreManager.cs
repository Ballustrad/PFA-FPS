using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float pointsRound;
    public float currentPoints;

    
    public GameManager gameManager;
    public TextMeshProUGUI textScore;
    public MiddleManScenehandler middleManScenehandler;


    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        gameManager = GameManager.Instance;
        middleManScenehandler.currentPoint = middleManScenehandler.currentPoint + middleManScenehandler.pointOfRound;
        currentPoints = middleManScenehandler.currentPoint;
        UpdateScoreTextTotal();
    }

    public void UpdateScoreTextRound()
    {
        if (textScore != null) 
        {
            textScore.text = pointsRound.ToString();
        }
    }
    public void UpdateScoreTextTotal()
    {
        if(textScore != null)
        {
            textScore.text = currentPoints.ToString();  
        }
    }
    public void AddPoints(float amount)
    {
        pointsRound += amount;
        UpdateScoreTextRound();
    }

    public void LobbyLoad()
    {
        middleManScenehandler.pointOfRound = pointsRound;
        
        
    }


}
