using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float destroyDelay = 1f; // D�lai avant la destruction du texte des d�g�ts

    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        // D�marrer une coroutine pour d�truire le texte des d�g�ts apr�s un certain d�lai
        StartCoroutine(DestroyTextAfterDelay());
    }

    private IEnumerator DestroyTextAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
