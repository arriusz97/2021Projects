using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Effects;
using UnityEngine;
using WaterSystem;

public class underWater : MonoBehaviour
{
    [Header("Underwater")]
    public GameObject m_Camera;
    public GameObject m_player;
    public SwimTrigger m_swimTrigger;
    public GameObject m_GlobalVolume;
    public GameObject m_bubble;

    [Header("Water")]
    public Water _water;

    //[Header("Water Sound")]
    //public AudioSource m_Abovewater;
    ////public AudioSource m_Insidewater;
    //public bool m_underwater;


    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false; //close fog
        m_GlobalVolume.SetActive(false);
        m_bubble.SetActive(false);
        // m_Abovewater.Play();

        _water.surfaceData._basicWaveSettings.amplitude = 4f;
        _water.surfaceData._basicWaveSettings.wavelength = 50f;
        _water.Init();

        Debug.Log("underwater start 시작");

    }

    // Update is called once per frame
    void Update()
    {
        //player가 물에 들어갔다면 fog -> true
        if (m_swimTrigger.m_isWater && m_player.transform.position.y < -23.5f)
        {        
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.5f, 0.55f);
            RenderSettings.fogDensity = 0.015f;
            m_GlobalVolume.SetActive(true);
            m_bubble.SetActive(true);

          //  m_Abovewater.Stop();
        }
        else
        {
            RenderSettings.fog = false;
            m_GlobalVolume.SetActive(false);
            m_bubble.SetActive(false);

        }

    }

}
