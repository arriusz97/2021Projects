using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField]
    private float SecondsOfDay = 60f;               //하루의 길이(초)
    [SerializeField]
    private float currentTime = 0.3f;               //현재 시간 0일때 자정, 0.3일때 아침
    [SerializeField]
    private int currentDay = 0;                     //날짜 count
    [SerializeField]
    private GameObject mSun, mMoonState, mMoon;     //태양, 달위치, 달
    [SerializeField]
    private Camera mCam;                            //Player 카매라


    // Update is called once per frame
    void Update()
    {
        LightRotation();

        currentTime += (Time.deltaTime / SecondsOfDay);
        if (currentTime >= 1)
        {
            currentTime = 0;                        //자정이 지나면
            currentDay++;                           //날짜 증가
        }
    }


    private void LightRotation()
    {
        mSun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);     
        //시간에 따른 태양빛의 방향 변화
        mMoonState.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 100, 170, 0);
        //시간에 따른 달의 위치 변화

        mMoon.transform.LookAt(mCam.transform);     //달이 항상 Player를 바라봐서 평면 이미지임을 숨김
    }

}
