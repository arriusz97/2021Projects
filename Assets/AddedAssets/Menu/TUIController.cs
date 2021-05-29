using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUIController : MonoBehaviour
{
    public static TUIController tuiInstance;

    private void Awake()
    {
        if (tuiInstance == null)
        {
            tuiInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
