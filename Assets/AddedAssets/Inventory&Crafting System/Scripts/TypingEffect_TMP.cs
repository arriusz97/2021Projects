using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect_TMP : MonoBehaviour
{

    public Text ChatText; // 실제 채팅이 나오는 텍스트

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string[] fulltext;

    public string writerText = "";

    public float WaitSeconds = 0.1f;

    public bool m_isTyping; //typing 되고 있는 동안 true 될 bool 변수

    bool isButtonClicked = false;

    public RawImage m_background;
    public RawImage m_outline;

    public void StartNarration()
    {
        StartCoroutine(TextPractice());
        m_isTyping = true;
    }

    void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
                Debug.Log("key pressed");
            }
        }

        if (!m_isTyping)
        {
            this.gameObject.SetActive(false);
            this.transform.parent.gameObject.SetActive(false);
            m_background.gameObject.SetActive(false);
            m_outline.gameObject.SetActive(false);
        }

    }


    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        //CharacterName.text = narrator;
        writerText = "";

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            yield return new WaitForSeconds(WaitSeconds);
            writerText += narration[a];
            ChatText.text = writerText;
          //  Debug.Log("narration text = " + narration[a]);
            yield return null;
        }

        Debug.Log(m_isTyping);

        //키를 다시 누를 떄 까지 무한정 대기
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        for (int i = 0; i < fulltext.Length; i++)
        {
            yield return StartCoroutine(NormalChat("캐릭터1", fulltext[i]));
            m_isTyping = true;
        }

        //빠져나왔다면 text가 끝난 것 -> false
        m_isTyping = false;

        Debug.Log(m_isTyping + "typing finish");
    }
}