using UnityEngine;

public class ParticleBurst : MonoBehaviour
{

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = true;
        emission.SetBursts(
        new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0.0f, 10),
            new ParticleSystem.Burst(1.0f, 50),
            new ParticleSystem.Burst(2.0f, 100)
        });
        ps.Play();
    }
}
