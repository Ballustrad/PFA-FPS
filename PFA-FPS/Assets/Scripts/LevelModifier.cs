using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelModifier : MonoBehaviour
{
    public MiddleManScenehandler middleManScenehandler;
    public GameObject melee;
    public GameObject kamikaze;
    public GameObject ranged;
    public GameObject tpHandler;
    public GameManager manager;




    private void Awake()
    {
        manager = GameManager.Instance;
        if (middleManScenehandler.indexLevel == 1 )
        {
           
        }
        if (middleManScenehandler.indexLevel == 2)
        {

        }
        if (middleManScenehandler.indexLevel == 3)
        {
            kamikaze.gameObject.GetComponent<NavMeshAgent>().speed += 7;
            melee.gameObject.GetComponent<NavMeshAgent>().speed += 7;
            ranged.gameObject.GetComponent<NavMeshAgent>().speed += 7;
        }
        if (middleManScenehandler.indexLevel == 4)
        {
            SceneManager.LoadScene("LevelNoLight");
        }
        if (middleManScenehandler.indexLevel == 5)
        {
            tpHandler.SetActive(true);
        }
        if (middleManScenehandler.indexLevel == 6)
        {
            manager.spawnInterval = 1f;
        }
        if (middleManScenehandler.indexLevel == 7)
        {
            manager.healthDecay = true;  
        }
    }
    private void OnDestroy()
    {
        if (middleManScenehandler.indexLevel == 1)
        {

        }
        if (middleManScenehandler.indexLevel == 2)
        {

        }
        if (middleManScenehandler.indexLevel == 3)
        {
            kamikaze.gameObject.GetComponent<NavMeshAgent>().speed -= 7;
            melee.gameObject.GetComponent<NavMeshAgent>().speed -= 7;
            ranged.gameObject.GetComponent<NavMeshAgent>().speed -= 7;
        }
        if (middleManScenehandler.indexLevel == 4)
        {
            
        }
        if (middleManScenehandler.indexLevel == 5)
        {
            tpHandler.SetActive(false);
        }
        if (middleManScenehandler.indexLevel == 6)
        {
            manager.spawnInterval = 5f;
        }
        if (middleManScenehandler.indexLevel == 7)
        {
            manager.healthDecay = false;
        }
    }
}
