using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

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
    [SerializeField]
    private GameObject m_particle;


    public yachtCtrl m_yachtCtrl;

    [Header("Typing effect")]
  //  public TypingEffect m_tutorialTyping;
    public TypingEffect_TMP m_Message01_typing;
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
    [SerializeField]
    private TutorialSceneCtrl tutorial_Scene_Ctrl;


    //private bool b_NarrationRunning = false;
    public bool b_SceneChagne;  //scene을 변경할 bool 변수

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        tutorial_Scene_Ctrl = GameObject.Find("TutorialSceneCtrl").gameObject.GetComponent<TutorialSceneCtrl>();
        m_Message01_typing.StartNarration();
    }

    private void sceneChange()
    {
        m_Message02.gameObject.SetActive(true);
        m_Message02_background.gameObject.SetActive(true);
        m_Message02_outline.gameObject.SetActive(true);
        m_Message02_text.gameObject.SetActive(true);
    }

    //player가 storm trigger에 들어가면 부를 함수
    public void Message02_func()
    {
        StartCoroutine(Message02_Start());
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
            m_particle.gameObject.SetActive(false);
        }

        //천둥이 치고 15초가 지났다면
        if (b_SceneChagne)
        {
            m_yachtCtrl.m_StormSound.volume = 1.0f;
            m_SceneChange_Image.gameObject.SetActive(true);
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Press Escape");
            tutorial_Scene_Ctrl.StartLoadTitleScene();
        }
    }


    IEnumerator Message02_Start()
    {
        //폭풍우 치고 3초 뒤 나레이션02 시작
        WaitForSeconds three = new WaitForSeconds(3.0f);
        yield return three;

        Debug.Log("Coroutine 시작하고 3초 지남");

        //message02 set active true
        sceneChange();

        //한번만 발동되게 하기
        m_Message02_typing.StartNarration();
        Debug.Log("message 02 시작");

        StartCoroutine(SceneChange());
    }

    //storm이 시작되고, player가 배가 조종되지 않는 다는 것을 알게된 후 배 위를 돌아다니다가
    //15초 뒤 천둥이 요트 위에 치게 되고 Scene change
    IEnumerator SceneChange()
    {
        Debug.Log("SceneChange Coroutine 불려짐");
        WaitForSeconds fift = new WaitForSeconds(15.0f);
        yield return fift;

        b_SceneChagne = true;
    }
}
