using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //tutorial manager
    //1. thunder, rain 관리해서 on, off 해주기 -> 아니면 bool 변수로 넘겨주기
    //2. UI manager 생성해서 UI 만들어주기

    //yacht에 thunder치면 gameScene으로 넘어가게 하기


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

    public yachtCtrl m_yachtCtrl;

    [Header("Typing effect")]
    public TypingEffect m_tutorialTyping;
    public TypingEffect_TMP m_tmpro_typing;

    private void Start()
    {
        // m_tutorialTyping.StartNarration();
       // m_tmpro_typing.StartNarration();
    }

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

        //천둥이 쳤다면
        if (m_yachtCtrl.b_IsThunder)
        {
            SceneManager.LoadScene(2);
        }
    }
}
