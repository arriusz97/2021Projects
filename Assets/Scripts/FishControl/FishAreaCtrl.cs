using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAreaCtrl : MonoBehaviour
{
    public bool m_isTurning = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            m_isTurning = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            m_isTurning = false;
        }
    }
}
