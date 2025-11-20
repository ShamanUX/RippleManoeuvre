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
    public float minLifeTime = 1f;
    public float maxLifeTime = 2f;
    public float minScale = 3f;
    public float maxScale = 8f;
    public float minRadius = 0.5f;
    public float maxRadius = 2f;

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
        main.startLifetime = GetComponent<EnlargeAndFade>().rippleLifetime;
        Debug.Log("start lifetime: " + main.startLifetime.constant);

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


    public void SetStrength(float holdTime)
    {
        float maxHoldtime = 2f;
        float scaleModifier = Mathf.Lerp(minScale, maxScale, holdTime / maxHoldtime);
        float rippleLifetime = Mathf.Lerp(minLifeTime, maxLifeTime, holdTime / maxHoldtime);
        rippleEffectiveRadius = Mathf.Lerp(minRadius, maxRadius, holdTime / maxHoldtime);
        GetComponent<EnlargeAndFade>().scaleModifier = scaleModifier;
        GetComponent<EnlargeAndFade>().rippleLifetime = rippleLifetime;


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
                    Invoke(nameof(DestroyRipple), 0.2f);
                }

            }
        }
    }

    void DestroyRipple()
    {
        Destroy(gameObject);
    }
}
