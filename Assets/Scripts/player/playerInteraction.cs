using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    //물고기와의 상호작용 (잡아 먹을 수 있는) Ctrl

    public AudioSource m_pickupSound;

    private void Awake()
    {
        m_pickupSound.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        //만약 amberjack과 충돌하고 f키를 눌렀다면 습득
        if (other.gameObject.CompareTag("Amberjack"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Amberjack");
                //amberjack inventory에 넣기
            }
        }
        else if(other.gameObject.CompareTag("Salmon"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Salmon");
                //salmon inventory에 넣기
            }
        }
        else if (other.gameObject.CompareTag("Bass"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Bass");
                //bass inventory에 넣기
            }
        }
        else if(other.gameObject.CompareTag("Goldfish"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Goldfish");
                //goldfish inventory에 넣기
            }
        }
        else if (other.gameObject.CompareTag("Koi"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Koi");
                //Koi inventory에 넣기
            }
        }
        else if (other.gameObject.CompareTag("Koi2"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                m_pickupSound.Play();
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Koi2");
                //Koi2 inventory에 넣기
            }
        }
    }
}
