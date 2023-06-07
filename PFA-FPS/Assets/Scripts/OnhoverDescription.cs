using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnhoverDescription : MonoBehaviour
{
    [Header("TextTip")]
    public GameObject description;
    public void PointerEnter()
    {
        description.SetActive(true);

    }
    public void PointerExit()
    {
        description.SetActive(false);
    }
}
