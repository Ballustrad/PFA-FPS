using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverTip : MonoBehaviour
{
    [Header("TextTip")]
    public GameObject barre;
    public void PointerEnter()
    {
        barre.GetComponent<Image>().enabled = true;

    }
    public void PointerExit()
    {
        barre.GetComponent<Image>().enabled = false;
    }
}
