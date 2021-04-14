using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Effects;
using UnityEngine;

public class underWater : MonoBehaviour
{
    public GameObject m_Camera;
    public GameObject m_player;
    public SwimTrigger m_swimTrigger;
    public GameObject m_GlobalVolume;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false; //close fog
        m_GlobalVolume.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //player가 물에 들어갔다면 fog -> true
        if (m_swimTrigger.m_isWater)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.5f, 0.55f);    // 3 4 7
            RenderSettings.fogDensity = 0.025f;
            m_GlobalVolume.SetActive(true);
        }
        else
        {
            RenderSettings.fog = false;
            m_GlobalVolume.SetActive(false);
        }
    }
}
