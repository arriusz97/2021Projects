using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTimerType
{
    HP,
    TP,
}

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private CircularTimer mHP, mTP;

    private CircularTimer mUpdateTimer;

    //타이머 시작
    private void StartTimer(int TimerType)
    {
        if (TimerType == 0)
        {
            mHP.StartTimer();
        }
        else if (TimerType == 1)
        {
            mTP.StartTimer();
        }
    }

    private void PauseTimer(int TimerType)
    {
        if (TimerType == 0)
        {
            mHP.PauseTimer();
        }
        else if (TimerType == 1)
        {
            mTP.PauseTimer();
        }
    }

    //
    private void UpdateTimer(int TimerType, float Update)
    {
        if (TimerType == 0)
        {
            mUpdateTimer = mHP;
        }
        else if (TimerType == 1)
        {
            mUpdateTimer = mTP;
        }
        mUpdateTimer.CurrentTime = Mathf.Clamp(mUpdateTimer.CurrentTime - Update, 0, mUpdateTimer.duration);
    }

    private void Start()
    {
        StartTimer((int)eTimerType.HP);
        StartTimer((int)eTimerType.TP);
    }
}
