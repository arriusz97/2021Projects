using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTrigger : MonoBehaviour
{
    public bool m_Storm_Start;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Yacht"))
        {
            m_Storm_Start = true;
        }
    }
}
