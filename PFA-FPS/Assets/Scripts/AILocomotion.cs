using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public GameManager gameManager;
    public Transform playerTransform;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();

        playerTransform = gameManager.currentPlayer;

    }

    
    // Update is called once per frame
    void Update()
    {
        
        agent.destination = playerTransform.position;
       
    }
}
