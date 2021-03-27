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
    private CircularTimer mUpdateTimer;

    public List<CircularTimer> TimerContainer = new List<CircularTimer>();

    private void StartTimer(int TimerType)
    {
        TimerContainer[TimerType].StartTimer();
    }

    private void PauseTimer(int TimerType)
    {        
         TimerContainer[TimerType].PauseTimer();     
    }

    //
    private void UpdateTimer(int TimerType, float Update)
    {
        TimerContainer[TimerType].CurrentTime = Mathf.Clamp(mUpdateTimer.CurrentTime - Update, 0, mUpdateTimer.duration);
    }

    private void Start()
    {
        StartTimer((int)eTimerType.HP);
        StartTimer((int)eTimerType.TP);
    }
}
