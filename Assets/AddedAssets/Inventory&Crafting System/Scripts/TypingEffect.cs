using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{

    public Text ChatText; 

    public List<KeyCode> skipButton;

    public string[] fulltext;

    public string writerText = "";

    public float WaitSeconds = 0.05f;

    bool isButtonClicked = false;

    public bool waiting, narrationEnd = false;

    public void StartNarration(int startLine, int finishLine)
    {
        StartCoroutine(TextPractice(startLine, finishLine));
    }

    void Update()
    {
        Skip();
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        narrationEnd = false;
        writerText = "";

        for (a = 0; a < narration.Length; a++)
        {
            if(!narration[a].Equals(" "))
            {
                yield return new WaitForSeconds(WaitSeconds);
                writerText += narration[a];
                ChatText.text = writerText;
                yield return null;
            }
            
        }

        narrationEnd = true;

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

    IEnumerator TextPractice(int startLine, int finishLine)
    {
        waiting = false;
        for (int i = startLine; i <= finishLine; i++)
        {
            if (i == finishLine)
            {
                waiting = true;
            }
            yield return StartCoroutine(NormalChat("캐릭터1", fulltext[i]));
        }
        
    }

    public void Skip()
    {
        foreach (var element in skipButton)
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
    }
}