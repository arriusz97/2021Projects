using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain5_6_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain5_6_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain5_6_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain5_6_Bgm = false;
        }
    }
}
