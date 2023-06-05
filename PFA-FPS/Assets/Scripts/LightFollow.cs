using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public Vector3 offset; // Décalage de position par rapport au joueur
    public GameManager manager;
    private void Awake()
    {
        manager = GameManager.Instance;

        player = manager.player.transform;
    }
    private void Update()
    {
        // Définir la position de la lumière sur le joueur avec le décalage
        transform.position = player.position + offset;
    }
}
