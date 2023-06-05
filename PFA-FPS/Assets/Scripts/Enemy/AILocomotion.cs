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
    void Awake()
    {
        gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();

        if (gameManager != null)
        {
            playerTransform = gameManager.currentPlayer;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if ( playerTransform != null && timer < 0.0f)
        {
            if (playerTransform != null && timer < 0.0f)
            {
                if (!GetComponent<Target>().IsParalyzed)
                {
                    if (playerTransform != null && playerTransform.gameObject.activeSelf)
                    {
                        float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
                        if (sqDistance > maxDistance * maxDistance)
                        {
                            agent.destination = playerTransform.position;
                        }
                    }
                }
                    

                timer = maxTime;
            }
        }
           
    }
}
