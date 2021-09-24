using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //tutorial manager
    //1. thunder, rain 관리해서 on, off 해주기 -> 아니면 bool 변수로 넘겨주기
    //2. UI manager 생성해서 UI 만들어주기
    //3. skybox 교체해주기 -> 특정 event를 달성 했을 경우

    [Header("SkyBox")]
    [SerializeField]
    private Material m_SunnySkyBox;
    [SerializeField]
    private Material m_RainSkyBox;
    [SerializeField]
    private Light m_Light;

    [Header("Trigger")]
    [SerializeField]
    private StormTrigger m_StormTrigger;

    [Header("Thunder")]
    [SerializeField]
    private GameObject[] m_Thunder;

    private void Update()
    {

        //storm trigger에 들어가면 skybox 변경해주고, thunder 켜주기
        if (m_StormTrigger.m_Storm_Start)
        {
            for(int i=0; i<m_Thunder.Length; i++)
            {
                m_Thunder[i].SetActive(true);
            }

            RenderSettings.skybox = m_RainSkyBox;
            m_Light.color = Color.gray;
        }
    }
}
