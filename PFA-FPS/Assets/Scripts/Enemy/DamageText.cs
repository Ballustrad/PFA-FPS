using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float destroyDelay = 1f; // Délai avant la destruction du texte des dégâts

    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        // Démarrer une coroutine pour détruire le texte des dégâts après un certain délai
        StartCoroutine(DestroyTextAfterDelay());
    }

    private IEnumerator DestroyTextAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
