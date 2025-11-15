using UnityEngine;

public class ControlColliderAndParticles : MonoBehaviour
{

    private float scaleToSpeedRatio = 2.1164f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float rippleScaleModifier = GetComponent<EnlargeAndFade>().scaleModifier;
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = rippleScaleModifier / scaleToSpeedRatio;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
