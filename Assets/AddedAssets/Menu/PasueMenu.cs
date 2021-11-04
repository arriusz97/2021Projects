using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;

    [SerializeField]
    private DataController dataController;

    private bool isActive = false;

    private void OnEnable()
    {
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
    }

    public void ClickLoad()
    {
        dataController.LoadGameData();
    }

    public void ClickQuit()
    {
        StartCoroutine(TitleLoadCoroutine());
    }

    public void ClickResume()
    {
        gameObject.SetActive(false);
        actionController.Lock(false);
    }

    public void Escape()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
        if (isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(actionController == null)
        {
            actionController = FindObjectOfType<ActionController>();
        }
        actionController.Lock(isActive);
    }

    IEnumerator TitleLoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("TitleScene");

        while (!operation.isDone)
        {
            yield return null;
        }

        GameObject.Find("TitleUI").transform.Find("TUI").gameObject.SetActive(true);
        GameObject.Find("UI").transform.Find("DayCounter").gameObject.SetActive(false);
        GameObject.Find("UI").SetActive(false);

        actionController.OnQuit();
        gameObject.SetActive(false);
    }
}
