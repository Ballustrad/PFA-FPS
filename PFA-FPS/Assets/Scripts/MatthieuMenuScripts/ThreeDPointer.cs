using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDPointer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pointerEnterSound;

    public Transform newPosition;
    public Transform initialPosition;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void ThreeDPointerEnter()
    {
        Debug.Log("Pointer Enter");
        audioSource.PlayOneShot(pointerEnterSound);
        Vector3 newPositionX = new Vector3(newPosition.position.x, originalPosition.y, originalPosition.z);
        transform.position = newPositionX;
    }

    public void ThreeDPointerExit()
    {
        Debug.Log("Pointer Exit");
        Vector3 initialPositionX = new Vector3(initialPosition.position.x, originalPosition.y, originalPosition.z);
        transform.position = initialPositionX;
    }
}
