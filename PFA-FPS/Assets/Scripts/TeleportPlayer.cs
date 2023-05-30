using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public float teleportInterval = 10f; // Intervalle de t�l�portation en secondes
    public Vector3 teleportRangeMin; // Plage minimale de t�l�portation
    public Vector3 teleportRangeMax; // Plage maximale de t�l�portation
    public GameManager gameManager;
    private float timer; // Compteur de temps pour le t�l�porteur
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= teleportInterval)
        {
            TpPlayer();
            timer = 0f;
        }
    }

    private void TpPlayer()
    {
        if (gameManager.player != null)
        {
            Vector3 randomPosition = GetRandomPosition();
            gameManager.player.transform.position = randomPosition;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(teleportRangeMin.x, teleportRangeMax.x);
        float randomY = Random.Range(teleportRangeMin.y, teleportRangeMax.y);
        float randomZ = Random.Range(teleportRangeMin.z, teleportRangeMax.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
