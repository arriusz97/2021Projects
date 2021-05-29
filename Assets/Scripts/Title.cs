using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameScene2";

 //   public static Title instance;

    [SerializeField]
    private DataController dataController;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //        Destroy(gameObject);
    //}

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);
    }

    public void ClickLoad()
    {
        StartCoroutine(GameLoadCoroutine());
    }

    public void ClickExit()
    {
        Application.Quit();
    }
    IEnumerator GameLoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        dataController = FindObjectOfType<DataController>();
        dataController.LoadGameData();

        GameObject.Find("GUI").transform.Find("UI").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
