using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain8_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain8_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain8_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain8_Bgm = false;
        }
    }
}
