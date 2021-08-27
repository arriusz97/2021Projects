﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private player Player;

    [SerializeField]
    private float O2Recover, groundY;

    public List<CircularTimer> TimerContainer = new List<CircularTimer>();

    public void StartTimer(int TimerNum)
    {
        TimerContainer[TimerNum].StartTimer();
    }

    public void PauseTimer(int TimerNum)
    {        
         TimerContainer[TimerNum].PauseTimer();     
    }

    public void StopTimer(int TimerNum)
    {
        TimerContainer[TimerNum].StopTimer();
    }

    public void UpdateTimer(int TimerNum, float Update)
    {
        TimerContainer[TimerNum].CurrentTime = Mathf.Clamp(TimerContainer[TimerNum].CurrentTime - Update, 0, TimerContainer[TimerNum].duration);
    }

    private void Start()
    {
        StartTimer(0);
        StartTimer(1);
        StopTimer(2);
        TimerContainer[2].Activated(false);        
    }

    private void Update()
    {
        if (Player.transform.localPosition.y <= groundY)
        {
            TimerContainer[2].Activated(true);
            StartTimer(2);
        }
        else
        {
            if(TimerContainer[2].CurrentTime <= 0.1)
            {
                TimerContainer[2].Activated(false);
                StopTimer(2);               
            }
            else
            {
                UpdateTimer(2, O2Recover+1.0f * Time.deltaTime);
            }
        }
        if(TimerContainer[3].isPaused)
        {
            StopTimer(3);
            TimerContainer[3].Activated(false);
        }
        UpdateTimer(0, Time.deltaTime);
    }

    public void ActionClockOn(int _duration)
    {
        TimerContainer[3].duration = _duration;
        TimerContainer[3].Activated(true);
        StartTimer(3);
    }

    public float GetDuration(int timerNum)
    {
        return TimerContainer[timerNum].duration;
    }

    public float GetCurrentTime(int timerNum)
    {
        return TimerContainer[timerNum].CurrentTime;
    }

    public void SetCurrentTime(int timerNUm, float currentTime)
    {
        TimerContainer[timerNUm].CurrentTime = currentTime;
    }
}
