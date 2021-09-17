using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public string sceneName = "GameScene2";

    [SerializeField]
    private GameObject background;

    private float time = 0;
 //   public static Title instance;

    [SerializeField]
    private DataController dataController;

    [SerializeField]
    private Image progressbar;

    private bool Load = false;
    public void ClickStart()
    {
        Load = false;
        progressbar.gameObject.SetActive(true);
        background.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        StartCoroutine(GameStartCoroutine());
    }

    public void ClickLoad()
    {
        Load = true;
        progressbar.gameObject.SetActive(true);
        background.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        StartCoroutine(GameStartCoroutine());
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    IEnumerator GameStartCoroutine()
    {
        progressbar.fillAmount = 0f; 
        //yield return StartCoroutine(Fade(true));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;


        float pgtimer = 0.0f;
        while (!operation.isDone)
        {
            yield return null;
            pgtimer += Time.unscaledDeltaTime;

            if(operation.progress < 0.9f)
            {
                progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, operation.progress, pgtimer);
                //progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, 0.9f, pgtimer);
                if (progressbar.fillAmount >= operation.progress)
                {
                    pgtimer = 0f;
                }
            }
            else
            {
                progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, 1f, pgtimer);
                if(progressbar.fillAmount == 1.0f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
                
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == sceneName)
        {
            ProgressEnd();
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private void ProgressEnd()
    {

        if (GameObject.Find("GUI"))
            GameObject.Find("GUI").transform.Find("UI").gameObject.SetActive(true);

        if (Load)
        {
            dataController = FindObjectOfType<DataController>();
            dataController.LoadGameData();
        }

        progressbar.gameObject.SetActive(false);
        background.SetActive(false);
        gameObject.SetActive(false);
    }
}
