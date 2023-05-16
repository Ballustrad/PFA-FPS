using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip PointerEnterSound;
    public Button button;
    


    public void PointerEnter()
    {
        audioSource.PlayOneShot(PointerEnterSound);
    }

   

}