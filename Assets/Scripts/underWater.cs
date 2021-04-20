﻿using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Effects;
using UnityEngine;

public class underWater : MonoBehaviour
{
    [Header("Underwater")]
    public GameObject m_Camera;
    public GameObject m_player;
    public SwimTrigger m_swimTrigger;
    public GameObject m_GlobalVolume;
    public GameObject m_bubble;

    [Header("Above water Sound")]
    public AudioSource m_Abovewater;


    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false; //close fog
        m_GlobalVolume.SetActive(false);
        m_bubble.SetActive(false);
       // m_Abovewater.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        //player가 물에 들어갔다면 fog -> true
        if (m_swimTrigger.m_isWater && m_player.transform.position.y < -21.5)
        {        
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.5f, 0.55f);    // 3 4 7
            RenderSettings.fogDensity = 0.015f;
            m_GlobalVolume.SetActive(true);
            m_bubble.SetActive(true);

            m_Abovewater.Stop();
        }
        else
        {
            RenderSettings.fog = false;
            m_GlobalVolume.SetActive(false);
            m_bubble.SetActive(false);

            //m_Abovewater.mute = false;
            m_Abovewater.Play();
        }

    }
}
