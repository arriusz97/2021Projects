using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class yachtDrivingSit : MonoBehaviour
{
    public bool m_playerEnter = false;
    public TextMeshProUGUI m_sitText;
    public playerCtrl_tutorial m_playerTutorial;

    private void Update()
    {
        if (m_playerTutorial.m_isSit)
        {
            m_sitText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //안내 UI 띄우기
            m_sitText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Sit ";
            m_sitText.gameObject.SetActive(true);
            m_playerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //안내 UI 끄기
            m_sitText.gameObject.SetActive(false);
            m_playerEnter = false;
        }
    }

}
