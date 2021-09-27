using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCtrl : MonoBehaviour
{
    public SunController m_sunCtrl;
    public Material m_CloudMaterial;

    private void Update()
    {
        //밤이라면
        if (m_sunCtrl.m_night)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            //낮이라면

            if (!this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
