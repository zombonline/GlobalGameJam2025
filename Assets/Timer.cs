using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    bool isRunning = false;
    public delegate void TimerCompleteDelegate();
    public event TimerCompleteDelegate onTimerComplete;


    [SerializeField] private TextMeshProUGUI textTime;
    private void Update()
    {
        DisplayTime();
        if (!isRunning) { return; }
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            isRunning = false;
            onTimerComplete?.Invoke();
        }
    }

    public void SetTime(float time)
    {
        currentTime = time;
    }
    public void StartTimer()
    {
        isRunning = true;
    }
    public void PauseTimer()
    {
        isRunning = false;
    }

    public void DisplayTime()
    {
        if (textTime == null)
            return;

        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);
        textTime.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}
