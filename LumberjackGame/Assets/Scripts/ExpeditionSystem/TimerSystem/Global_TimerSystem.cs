using System;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Global_TimerSystem
{
    float timeToComplete;
    float elapsedTime;
    Action whenComplete;

    bool canRun = false;
    bool timerFinised;
    public bool TimerFinised { get { return timerFinised; } set { timerFinised = value; }}
    public bool CanRun { get { return canRun; } set { canRun = value; }}

    public void SetTimer(float _timeToComplete, Action _whenComplete, bool canStartTimer)
    {
        timeToComplete = _timeToComplete;
        elapsedTime = _timeToComplete;
        whenComplete += _whenComplete;
        canRun = canStartTimer;
        timerFinised = false;
    }

    public void StartTimer()
    {
        canRun = true;
    }

    public void Update(float deltaTime)
    {
        if(elapsedTime > 0 && canRun)
        {
            elapsedTime -= deltaTime;

            if(elapsedTime <= 0)
            {
                timerFinised = true;
                whenComplete?.Invoke();
            }
        }
    }

    public string UpdateTimer()
    {
        float minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}