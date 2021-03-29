using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private CircularTimer mUpdateTimer;

    public List<CircularTimer> TimerContainer = new List<CircularTimer>();

    private void StartTimer(int TimerNum)
    {
        TimerContainer[TimerNum].StartTimer();
    }

    private void PauseTimer(int TimerNum)
    {        
         TimerContainer[TimerNum].PauseTimer();     
    }

    //
    private void UpdateTimer(int TimerNum, float Update)
    {
        TimerContainer[TimerNum].CurrentTime = Mathf.Clamp(mUpdateTimer.CurrentTime - Update, 0, mUpdateTimer.duration);
    }

    private void Start()
    {
        for(int i=0; i < TimerContainer.Count; i++)
        {
            StartTimer(i);
        }
    }
}
