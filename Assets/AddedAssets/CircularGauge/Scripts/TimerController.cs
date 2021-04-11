using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private player Player;

    [SerializeField]
    private float O2Recover;

    public List<CircularTimer> TimerContainer = new List<CircularTimer>();

    private void StartTimer(int TimerNum)
    {
        TimerContainer[TimerNum].StartTimer();
    }

    private void PauseTimer(int TimerNum)
    {        
         TimerContainer[TimerNum].PauseTimer();     
    }

    private void StopTimer(int TimerNum)
    {
        TimerContainer[TimerNum].StopTimer();
    }

    private void UpdateTimer(int TimerNum, float Update)
    {
        TimerContainer[TimerNum].CurrentTime = Mathf.Clamp(TimerContainer[TimerNum].CurrentTime - Update*Time.deltaTime, 0, TimerContainer[TimerNum].duration);
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
        if (Player.m_isDive)
        {
            TimerContainer[2].Activated(true);
            StartTimer(2);
        }
        else
        {
            if(TimerContainer[2].CurrentTime <= 0)
            {
                StopTimer(2);
                TimerContainer[2].Activated(false);
            }
            else
            {
                UpdateTimer(2, O2Recover+1.0f);
            }
        }
    }
}
