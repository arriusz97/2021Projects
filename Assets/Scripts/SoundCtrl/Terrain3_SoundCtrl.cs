using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain3_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain3_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain3_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain3_Bgm = false;
        }
    }
}
