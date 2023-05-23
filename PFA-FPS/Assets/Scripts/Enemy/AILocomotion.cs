using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public GameManager gameManager;
    public Transform playerTransform;
    NavMeshAgent agent;

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;
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
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance*maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;
        }
        
       
    }
}
