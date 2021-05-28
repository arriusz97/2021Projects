using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTrigger : MonoBehaviour
{
    public bool m_isWater;

    [Header("UnderwaterSound")]
    public AudioSource m_Exitwater;
    public AudioSource m_Enterwater;


    private void Awake()
    {
        m_Enterwater.mute = true;
        m_Exitwater.mute = true;
    }

    //물에 들어간 것
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_isWater)
        {
            m_isWater = true;
            Debug.Log("player exit swimTrigger");
            m_Enterwater.mute = false;
            m_Enterwater.Play();
        }
    }

    //물에서 나온 것
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_isWater)
        {
            m_isWater = false;
            Debug.Log("player enter swimTrigger");
            m_Exitwater.mute = false;
            m_Exitwater.Play();
        }
    }
}
