using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;

    private bool isActive = false;

    public void ClickLoad()
    {
        actionController.LoadGame();
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
        GameObject.Find("UI").SetActive(false);
        /*
        GameObject.Find("UI").transform.Find("DayCounter").gameObject.SetActive(false);
        actionController.CloseCrafting();
        actionController.CloseInventory();
        */

        gameObject.SetActive(false);
    }
}
