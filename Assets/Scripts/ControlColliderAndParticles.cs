using UnityEngine;

public class ControlColliderAndParticles : MonoBehaviour
{

    private readonly float scaleToSpeedRatio = 2.03f;
    public float scaleToLastRippleBurstSpawnTimeRatio = 0.05f;
    public float rippleEffectiveRadius = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float rippleScaleModifier = GetComponent<EnlargeAndFade>().scaleModifier;
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = rippleScaleModifier / scaleToSpeedRatio;

        var emission = ps.emission;
        emission.enabled = true;

        float lastBurstSpawnTime = rippleEffectiveRadius * 2 / rippleScaleModifier;

        Debug.Log("Lastburst time:" + lastBurstSpawnTime);
        emission.SetBursts(
        new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0.0f, 500),
            new ParticleSystem.Burst(lastBurstSpawnTime, 500)
        });
        ps.Play();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
