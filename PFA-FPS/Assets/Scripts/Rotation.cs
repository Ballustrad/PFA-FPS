using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 10f; // Vitesse de rotation de l'objet

    private void Update()
    {
        // Fait tourner l'objet sur lui-même selon l'axe Y à une vitesse donnée
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
