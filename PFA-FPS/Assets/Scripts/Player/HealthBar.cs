using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;

    public void SetState(float current, float max)
    {
        float state = Mathf.Clamp01(current / max);
        bar.transform.localScale = new Vector3(state, 1f, 1f);
        bar.transform.localPosition = new Vector3((1f - state) * -0.5f, 0f, 0f);
    }
}
