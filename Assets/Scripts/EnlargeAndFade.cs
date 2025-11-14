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
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime / 3);
    }

    void ShrinkRipple()
    {
        gameObject.transform.localScale *= 1 + Time.deltaTime / 1.25f;

    }
}
