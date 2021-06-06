using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameScene2";

    [SerializeField]
    private GameObject tLoading, background, ui;

    private float time = 0;
 //   public static Title instance;

    [SerializeField]
    private DataController dataController;

    public void ClickStart()
    {
        tLoading.SetActive(true);
        background.SetActive(true);
        StartCoroutine(GameStartCoroutine());
    }

    public void ClickLoad()
    {
        tLoading.SetActive(true);
        background.SetActive(true);
        StartCoroutine(GameLoadCoroutine());
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    IEnumerator GameStartCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        if (GameObject.Find("GUI"))
            GameObject.Find("GUI").transform.Find("UI").gameObject.SetActive(true);

        
        while (!operation.isDone)
        {
            yield return null;            
        }

        tLoading.SetActive(false);
        background.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator GameLoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        if (GameObject.Find("GUI"))
            GameObject.Find("GUI").transform.Find("UI").gameObject.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }

        dataController = FindObjectOfType<DataController>();
        dataController.LoadGameData();

        
        tLoading.SetActive(false);
        background.SetActive(false);
        gameObject.SetActive(false);
    }
}
