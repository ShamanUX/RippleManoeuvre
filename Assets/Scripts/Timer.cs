using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text TimeDisplay;
    public Image Star;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        TimeDisplay.text = "TIME: " + ((int)_timer).ToString();
        if (!Star.gameObject.activeSelf && _timer >= 60)
        {
            Star.gameObject.SetActive(true);
        }
    }
}
