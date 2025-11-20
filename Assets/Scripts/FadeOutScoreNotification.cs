using UnityEngine;

public class FadeOutScoreNotification : MonoBehaviour
{
    private TMPro.TMP_Text scoreText;
    private float fadeDuration = 2f;
    private float fadeTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TMPro.TMP_Text>();
        fadeTimer = 0f;
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
        Color color = scoreText.color;
        color.a = alpha;
        scoreText.color = color;

        if (fadeTimer >= fadeDuration)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
