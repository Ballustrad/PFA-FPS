using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   

    
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
        
        if (gameManager.timerUi == null) 
        {
            UpdateScoreTextTotal();
        }
        
    }

    public void UpdateScoreTextRound()
    {
        if (textScore != null) 
        {
            textScore.text = middleManScenehandler.pointOfRound.ToString();
        }
    }
    public void UpdateScoreTextTotal()
    {
        if(textScore != null)
        {
            textScore.text = middleManScenehandler.currentPoint.ToString();  
        }
    }
    public void AddPoints(float amount)
    {
        middleManScenehandler.pointOfRound += amount;
        UpdateScoreTextRound();
    }

   


}
