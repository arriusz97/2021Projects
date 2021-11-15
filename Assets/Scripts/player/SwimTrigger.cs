using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTrigger : MonoBehaviour
{
    public bool m_isWater;

    [Header("UnderwaterSound")]
    public AudioSource m_Exitwater;
    public AudioSource m_Enterwater;
    public AudioSource m_Abovewater;


    private void Awake()
    {
        m_Enterwater.mute = true;
        m_Exitwater.mute = true;
        m_Abovewater.Play();
    }

    //물에 들어간 것
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_isWater)
        {
            m_isWater = true;
            m_Enterwater.mute = false;
            m_Enterwater.Play();
            m_Abovewater.Stop();
        }
    }

    //물에서 나온 것
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_isWater)
        {
            m_isWater = false;
            m_Exitwater.mute = false;
            m_Exitwater.Play();
            m_Abovewater.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_isWater)
        {
            m_isWater = false;
            m_Exitwater.mute = false;
            m_Exitwater.Play();
            m_Abovewater.Play();
        }
    }
}
