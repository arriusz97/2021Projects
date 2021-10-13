using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameCtrl : MonoBehaviour
{
    
    public void playerDead()
    {
        StartCoroutine(dead());
    }

    public void endingScene()
    {
        StartCoroutine(ending());
    }

    //player가 죽었을 때 불릴 함수
    //player가 죽고나서 5초 뒤 lobby로 전환
    IEnumerator dead()
    {
        WaitForSeconds five  = new WaitForSeconds(5.0f);
        yield return five;

        SceneManager.LoadScene(0);
    }

    //player가 섬을 탈출했을 때 ending scene 연결
    IEnumerator ending()
    {
        yield return null;

        SceneManager.LoadScene(3);
    }
}
