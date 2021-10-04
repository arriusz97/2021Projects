using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class TUIController : MonoBehaviour
{
    [Header("Water")]
    public Water _water;

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

        _water.surfaceData._basicWaveSettings.amplitude = 4f;
        _water.surfaceData._basicWaveSettings.wavelength = 50f;

        _water.Init();
    }

    
}
