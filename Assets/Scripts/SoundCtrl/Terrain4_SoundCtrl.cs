using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain4_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain4_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain4_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain4_Bgm = false;
        }
    }
}
