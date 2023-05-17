using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDPointer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pointerEnterSound;
    public GameObject initialPosition;
    public GameObject newPosition;

   
        public void ThreeDPointerEnter(GameObject newPosition)
        {
        Debug.Log("Pointer Enter");
            audioSource.PlayOneShot(pointerEnterSound);
            Vector3 currentPosition = transform.position;
            currentPosition.z = newPosition.transform.position.z;
            transform.position = currentPosition;
        }

        public void ThreeDPointerExit(GameObject initialPosition)
        {
            Debug.Log("Pointer Exit");
            Vector3 currentPosition = transform.position;
            currentPosition.z = initialPosition.transform.position.z;
            transform.position = currentPosition;
        }
 }

