using UnityEngine;

public class EnlargeAndFade : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShrinkRipple();
        FadeOut();
    }

    void FadeOut()
    {
        Material material = gameObject.GetComponentInChildren<MeshRenderer>().material;
        Color lastColor = material.GetColor("_BaseColor");
        material.SetColor("_BaseColor", new Color(lastColor.r, lastColor.g, lastColor.b, lastColor.a - Time.deltaTime / 3));
    }

    void ShrinkRipple()
    {
        gameObject.transform.localScale *= 1 + Time.deltaTime / 1.25f;

    }
}
