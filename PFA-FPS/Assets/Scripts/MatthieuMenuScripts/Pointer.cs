using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip PointerEnterSound;
    public Button button;
    public float scaleFactor = 1.2f;
    private Vector3 originalScale;

    private void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
    }

    public void PointerEnter()
    {
        audioSource.PlayOneShot(PointerEnterSound);
        transform.localScale = originalScale * scaleFactor;
    }

    public void PointerExit()
    {
        transform.localScale = originalScale;
    }

}