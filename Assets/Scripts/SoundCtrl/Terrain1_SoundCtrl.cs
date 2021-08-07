using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain1_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain1_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain1_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain1_Bgm = false;
        }
    }
}
