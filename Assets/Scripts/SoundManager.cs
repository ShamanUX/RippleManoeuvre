using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioSource rippleSource;
    public float maxRipplePitch = 1.5f;
    public float minRipplePitch = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRippleSound(float holdTime)
    {
        float maxHoldtime = 2f;
        float pitch = Mathf.Lerp(maxRipplePitch, minRipplePitch, holdTime / maxHoldtime);
        rippleSource.pitch = pitch;
        rippleSource.Play();


    }
}
