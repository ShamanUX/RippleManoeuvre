using System;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeAndFade : MonoBehaviour
{

    public float scaleModifier = 0.5f;
    public float rippleLifetime = 1f;
    private float elapsedTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FadeOut();
        elapsedTime += Time.deltaTime;
        if (elapsedTime <= rippleLifetime)
        {
            EnlargeRipple();
        }
        else
        {
            GetComponent<ControlColliderAndParticles>().BeginShrinkEffectiveRadius();
        }
    }

    void FadeOut()
    {
        Material material = transform.Find("ForceSphere").GetComponent<MeshRenderer>().material;
        Color lastColor = material.GetColor("_BaseColor");
        float alpha = Mathf.Lerp(1, 0.3f, elapsedTime / rippleLifetime);

        material.SetColor("_BaseColor", new Color(lastColor.r, lastColor.g, lastColor.b, alpha));

    }

    void EnlargeRipple()
    {
        // gameObject.transform.localScale *= 1 + Time.deltaTime * scaleModifier;
        gameObject.transform.localScale += Vector3.one * (Time.deltaTime * scaleModifier);

    }
}
