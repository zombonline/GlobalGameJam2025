using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime, startTime;
    bool isRunning = false;
    public delegate void TimerCompleteDelegate();
    public event TimerCompleteDelegate onTimerComplete;


    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private Slider sliderTime;
    private void Update()
    {
        DisplayTime();
        DisplayTimeSlider();
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
        startTime = time;
    }
    public void StartTimer()
    {
        currentTime = startTime;
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
        mins = Mathf.Clamp(mins, 0, 59);
        secs = Mathf.Clamp(secs, 0, 59);
        textTime.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
    public void DisplayTimeSlider()
    {
        if(sliderTime == null)
        {
            return;
        }
        sliderTime.value = currentTime/startTime;
    }
}
