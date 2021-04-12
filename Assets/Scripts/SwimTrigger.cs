using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTrigger : MonoBehaviour
{
    public bool m_isWater;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_isWater)
        {
            m_isWater = true;
            Debug.Log("player exit swimTrigger -> start swimming");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_isWater)
        {
            m_isWater = false;
            Debug.Log("player enter swimTrigger -> stop swimming");
        }
    }
}
