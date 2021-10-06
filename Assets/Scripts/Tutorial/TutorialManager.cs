using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
  //  public TypingEffect m_tutorialTyping;
    public TypingEffect_TMP m_tmpro_typing;
    public TypingEffect_TMP m_Message02_typing;

    [Header("UI")]
    [SerializeField]
    private RawImage m_SceneChange_Image;
    [SerializeField]
    private GameObject m_Message02;
    [SerializeField]
    private GameObject m_Message02_outline;
    [SerializeField]
    private GameObject m_Message02_background;
    [SerializeField]
    private Text m_Message02_text;
    private bool b_NarrationRunning = false;

    private void Start()
    {
         //m_tutorialTyping.StartNarration();
          m_tmpro_typing.StartNarration();
    }

    public void sceneChange()
    {
        m_Message02.gameObject.SetActive(true);
        m_Message02_background.gameObject.SetActive(true);
        m_Message02_outline.gameObject.SetActive(true);
        m_Message02_text.gameObject.SetActive(true);
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
            
            sceneChange();

            //typing이 끝났으면
            if (!m_Message02_typing.m_isTyping)
            {
                Debug.Log("Typing 끝");
                m_Message02.gameObject.SetActive(false);
                m_Message02_background.gameObject.SetActive(false);
                m_Message02_outline.gameObject.SetActive(false);
                m_Message02_text.gameObject.SetActive(false);
            }
            

        }

        //천둥이 치고 15초가 지났다면
        if (m_yachtCtrl.b_SceneChagne)
        { 

            m_SceneChange_Image.gameObject.SetActive(true);
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator SceneChange()
    {
        yield return null;

        //한번만 발동되게 하기
        m_Message02_typing.StartNarration();
    }
}
