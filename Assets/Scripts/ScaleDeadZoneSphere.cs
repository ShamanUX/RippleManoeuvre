using System;
using UnityEngine;

public class ScaleDeadZoneSphere : MonoBehaviour
{

    private float rippleEffectiveRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rippleEffectiveRadius = GetComponentInParent<ControlColliderAndParticles>().rippleEffectiveRadius;
        float scaleModifier = Math.Max(transform.parent.lossyScale.x - rippleEffectiveRadius * 2, 0);
        if (scaleModifier > 0)
        {
            scaleModifier /= transform.parent.lossyScale.x;
        }
        transform.localScale = Vector3.one * Math.Min(scaleModifier, 1);
    }
}
