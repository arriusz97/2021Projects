using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;

    private bool isActive = false;

    private Title title;

    public static PasueMenu Pinstance;

    private void Awake()
    {
        if (Pinstance == null)
        {
            Pinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

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
        actionController.Lock(!isActive);
    }

    IEnumerator TitleLoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("TitleScene");

        while (!operation.isDone)
        {
            yield return null;
        }

        title = FindObjectOfType<Title>();
        title.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
