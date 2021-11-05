using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField]
    private float SecondsOfDay = 60f;
    //하루의 길이(초), 

    public float currentTime = 0.3f; //현재 시간(0일때 자정, 0.25에서 일출, 0.75에서 일몰)

    public int currentDay = 0; //현재 날짜

    [SerializeField]
    private Light mSun;     //태양

    [SerializeField]
    private GameObject mMoonState, mMoon, mStarDome;     //달위치, 달, 천구

    [SerializeField]
    private Camera mCam;    //Player 카매라

    [SerializeField]
    private Material mStarMat;      //천구의 별

    [SerializeField]
    private DayCounter dayCounter;

    private bool dayCounterMove;

    //island bgm 밤, 낮 바꿔줄 bool 변수
    public bool m_night = false;

    [SerializeField]
    private DataController dataController;

    [SerializeField]
    private GameObject[] supplyBoxs = new GameObject[9];

    [SerializeField]
    private IngameCtrl ingameCtrl;

    private bool rescueSignal = false;

    // Start is called before the first frame update
    void Start()
    {
        //mStarMat = mStarDome.GetComponentInChildren<MeshRenderer>().material;  
        mStarMat.color = new Color(1f, 1f, 1f, 0f);
        dayCounter = GameObject.Find("GUI").transform.Find("UI").transform.Find("DayCounter").GetComponent<DayCounter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LightRotation();
        StarLight();
        currentTime += (Time.deltaTime / SecondsOfDay);
        DayUpdate();
        LightIntensity();
        NightCheck();
        DayCounterCall();
        SupplyBoxsReset();        
    }

    void DayCounterUpdate(int currentDay)       
    {
        dayCounter.gameObject.SetActive(true);
        dayCounter.DayUpdate(currentDay);
        //DayCounter를 활성화하고 현재 날짜를 전달

        dataController.Gamedata.currentDay = currentDay;
        dataController.SaveGameData();
        //Gamedata에 현재 날짜를 기록하고 저장
    }

    private void LightRotation()
    {
        mSun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        //시간에 따른 태양빛의 방향 변화
        mMoonState.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 100, 170, 0);
        //시간에 따른 달위치 변화
        mStarDome.transform.Rotate(new Vector3(0, 2f * Time.deltaTime, 0));      
        //시간에 따른 천구의 회전
        mMoon.transform.LookAt(mCam.transform);
        //달이 항상 Player를 바라봐서 평면 이미지임을 숨김

    }

    private void StarLight()
    {
        if (currentTime <= 0.23f || currentTime >= 0.75f) //저녁~새벽에만 별 보이게
        {
            mStarMat.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Time.deltaTime));
        }
        else if (currentTime <= 0.25f) //그 외의 시간에는 투명화
        {
            mStarMat.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, Time.deltaTime));
        }
    }

    private void LightIntensity()
    {
        if (currentTime <= 0.24)
        {
            gameObject.GetComponentInChildren<Light>().intensity = Mathf.Lerp(0.1f, 1.0f, currentTime);
        }
        else if (currentTime >= 0.76)
        {
            gameObject.GetComponentInChildren<Light>().intensity = Mathf.Lerp(1.0f, 0.1f, currentTime);
        }
        else
            gameObject.GetComponentInChildren<Light>().intensity = 1f;
    }

    public void SunControllerSetting()
    {       
        dayCounterMove = true;
        currentDay = dataController.Gamedata.currentDay;
        currentTime = 0.29f;      
    }

    private void NightCheck()
    {
        if (currentTime >= 0.75f || currentTime <= 0.25f)
        {
            m_night = true;
        }
        else
        {
            m_night = false;
        }
    }

    private void DayUpdate()
    {
        if(rescueSignal == true)
        {
            ingameCtrl.endingScene();
        }
        else if (currentTime >= 1)
        {
            currentTime = 0;                        //자정이 지나면
            currentDay++;                         //날짜 증가

            if (!dayCounterMove)            //날짜가 변하면 DayCounter 출력 준비
            {
                dayCounterMove = true;
            }
        }
    }

    private void DayCounterCall()
    {
        if (dayCounterMove && currentTime >= 0.3f)      // 아침이 되면 DayCounter 출력
        {

            if (!dayCounter.isActiveAndEnabled)
            {
                DayCounterUpdate(currentDay);
            }

            dayCounter.StartCounter();

            if (currentTime >= 0.3f + (5f / SecondsOfDay))       //5초간 출력되도록 한다.
            {
                dayCounter.gameObject.SetActive(false);
                dayCounterMove = false;
            }
        }
    }

    private void SupplyBoxsReset()
    {
        if (currentDay == 14)
        {
            for (int i = 0; i < supplyBoxs.Length; i++)
            {
                supplyBoxs[i].SetActive(true);
            }
        }
    }

    public void RescueCall()
    {
        rescueSignal = true;
    }
}
