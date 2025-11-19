using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text TimeDisplay;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        TimeDisplay.text = "TIME: " + ((int)_timer).ToString();
    }
}
