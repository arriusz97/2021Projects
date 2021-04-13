using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Effects;
using UnityEngine;

public class underWater : MonoBehaviour
{
    public GameObject m_Camera;
    public GameObject m_player;
    private float m_blurSpread = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false; //close fog
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
