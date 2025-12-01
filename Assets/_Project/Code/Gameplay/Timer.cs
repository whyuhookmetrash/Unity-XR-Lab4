using TMPro;
using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float _time;

    void Update()
    {
        _time += Time.deltaTime;
        _timerText.text = FormatTime(_time);
    }

    private string FormatTime(float timeInSeconds)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:00}:{1:00}:{2:00}",
                            timeSpan.Hours,
                            timeSpan.Minutes,
                            timeSpan.Seconds);
    }
}
