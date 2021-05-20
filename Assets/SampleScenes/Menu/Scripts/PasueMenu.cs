using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Application.Quit();
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
}
