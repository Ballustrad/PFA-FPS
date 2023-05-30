using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    private GameManager gameManager; // Référence vers le GameManager
    public MiddleManScenehandler middleManScenehandler;
    public string levelName;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene(levelName);
            middleManScenehandler.levelIsStarted = true;

            
        }
    }
}
