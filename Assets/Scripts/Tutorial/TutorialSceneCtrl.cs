using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneCtrl : MonoBehaviour
{
    public static TutorialSceneCtrl tutorial_Instance;

    private void Awake()
    {
        if (tutorial_Instance == null)
        {
            tutorial_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void StartLoadTitleScene()
    {
        StartCoroutine(TitleLoadCoroutine());
    }

    IEnumerator TitleLoadCoroutine()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync("TitleScene");

        while (!operation.isDone)
        {
            yield return null;
        }

        Cursor.visible = true;

        GameObject.Find("TitleUI").transform.Find("TUI").gameObject.SetActive(true);

        if (GameObject.Find("TitleUI").activeInHierarchy)
        {
            Debug.Log("Find Title UI----------2");
        }

    }
}
