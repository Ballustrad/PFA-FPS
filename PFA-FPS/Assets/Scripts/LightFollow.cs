using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform player; // R�f�rence au transform du joueur
    public Vector3 offset; // D�calage de position par rapport au joueur
    public GameManager manager;
    private void Awake()
    {
        manager = GameManager.Instance;

        player = manager.player.transform;
    }
    private void Update()
    {
        // D�finir la position de la lumi�re sur le joueur avec le d�calage
        transform.position = player.position + offset;
    }
}
