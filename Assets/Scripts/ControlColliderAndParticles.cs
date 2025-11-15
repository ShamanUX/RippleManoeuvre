using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ControlColliderAndParticles : MonoBehaviour
{

    private readonly float scaleToSpeedRatio = 2.03f;
    public float rippleEffectiveRadius = 2f;

    public float rippleBurstInterval = 2f;

    private bool beginShrinkEffectiveRadius = false;
    private bool isDestroying = false;

    public void BeginShrinkEffectiveRadius()
    {
        beginShrinkEffectiveRadius = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        float rippleScaleModifier = GetComponent<EnlargeAndFade>().scaleModifier;
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = rippleScaleModifier / scaleToSpeedRatio;
        main.startLifetime = GetComponent<EnlargeAndFade>().rippleAliveTime;

        var emission = ps.emission;
        emission.enabled = true;

        float lastBurstSpawnTime = rippleEffectiveRadius * 2 / rippleScaleModifier;

        int burstCount = (int)Math.Round((lastBurstSpawnTime / rippleBurstInterval) + 2); // +2 for the final burst and potential remainder
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[burstCount];

        int index = 0;
        for (float time = 0; time < lastBurstSpawnTime; time += rippleBurstInterval)
        {
            bursts[index] = new ParticleSystem.Burst(time, 500);
            index++;
        }

        bursts[index] = new ParticleSystem.Burst(lastBurstSpawnTime, 500);

        emission.SetBursts(bursts);
        ps.Play();
    }



    // Update is called once per frame
    void Update()
    {
        if (beginShrinkEffectiveRadius)
        {
            float rippleScaleModifier = GetComponent<EnlargeAndFade>().scaleModifier;
            rippleEffectiveRadius -= Time.deltaTime * rippleScaleModifier / 2;
            if (rippleEffectiveRadius <= 0)
            {
                if (!isDestroying)
                {
                    isDestroying = true;
                    Invoke(nameof(DestroyRipple), 0.5f);
                }

            }
        }
    }

    void DestroyRipple()
    {
        Destroy(gameObject);
    }
}
