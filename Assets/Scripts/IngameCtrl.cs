using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameCtrl : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup deadUICanvas, gui;

    private void OnEnable()
    {
        Cursor.visible = false;
        gui = GameObject.Find("GUI").GetComponent<CanvasGroup>();
    }

    public void playerDead()
    {
        Time.timeScale = 0;
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
        yield return StartCoroutine(Fade(true));
        gui.alpha = 1;
        WaitForSecondsRealtime five  = new WaitForSecondsRealtime(5.0f);
        yield return five;
        Cursor.visible = true;
        Time.timeScale = 1;

        GameObject.Find("TitleUI").transform.Find("TUI").gameObject.SetActive(true);
        GameObject.Find("UI").transform.Find("DayCounter").gameObject.SetActive(false);
        GameObject.Find("UI").SetActive(false);

        SceneManager.LoadScene(0);
    }

    //player가 섬을 탈출했을 때 ending scene 연결
    IEnumerator ending()
    {
        yield return null;

        SceneManager.LoadScene(3);
    }

    private IEnumerator Fade(bool isFadeIn) 
    { 
        float timer = 0f;
        gui.alpha = 0;
        while (timer <= 1f) 
        { 
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            deadUICanvas.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer); 
        }
        if (!isFadeIn) 
        { 
            gameObject.SetActive(false); 
        }
    }

}
