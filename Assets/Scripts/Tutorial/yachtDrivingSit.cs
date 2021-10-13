using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class yachtDrivingSit : MonoBehaviour
{
    public bool m_playerEnter = false;
    public bool m_playerSit = false;
    public TextMeshProUGUI m_sitText;
    public playerCtrl_tutorial m_playerTutorial;

    public GameObject m_player;

    //driving sit에 앉을 때 잴 timer
    public float m_Timer = 0.0f;

    private void Update()
    {
        //player stand up
        if(!m_player.activeInHierarchy && Input.GetKey(KeyCode.LeftControl) && m_Timer >= 0.5f)  //!m_player.activeInHierarchy
        {
            m_player.SetActive(true);
            m_playerSit = false;
            m_Timer = 0.0f;
            Debug.Log("m_Timer & player stand up : " + m_Timer);
        }
        
        if (m_playerEnter && Input.GetKey(KeyCode.F) && m_Timer <= 0.0f)   //player sit
        {
            m_player.SetActive(false);
            m_sitText.gameObject.SetActive(false);
            m_playerSit = true;
        }

        if (m_playerSit)
        {
            m_Timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //안내 UI 띄우기
            m_sitText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to sit";
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
