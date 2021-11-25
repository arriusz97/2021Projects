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

    public bool isActive = false;

    private void OnEnable()
    {
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
    }

    public void ClickLoad()
    {
        Escape(); 
        dataController.LoadGameData();
    }

    public void ClickQuit()
    {
        isActive = !isActive;
        TimeStop();
        CursorLock();
        StartCoroutine(TitleLoadCoroutine());
        
    }

    public void ClickResume()
    {
        Escape();        
    }

    public void Escape()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);

        if (isActive)
        {
            Cursor.visible = true;
        }
        else
            Cursor.visible = false;


        TimeStop();
        CursorLock();
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
    void TimeStop()
    {
        if (isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    void CursorLock()
    {
        if (actionController == null)
        {
            actionController = FindObjectOfType<ActionController>();
        }
        actionController.Lock(isActive);
    }
}
