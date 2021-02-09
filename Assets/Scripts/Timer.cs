using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timePassed;
    [SerializeField] Text timerText;
    bool keepTime = false;

    private void OnEnable()
    {
        EventManager.onStartGame += StartTimer;
        EventManager.onPlayerDeath += StopTimer;
    }    
    private void OnDisable()
    {
        EventManager.onStartGame -= StartTimer;
        EventManager.onPlayerDeath -= StopTimer;
    }

    private void Update()
    {
        if (keepTime)
        {
            timePassed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }
    void StartTimer()
    {
        timePassed = 0;
        keepTime = true;
    }

    void StopTimer()
    {
       keepTime = false;
    }
    void UpdateTimerDisplay()
    {
        float minutes;
        float seconds;

        minutes = Mathf.FloorToInt(timePassed / 60);
        seconds = timePassed % 60;

        timerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
    }
}
