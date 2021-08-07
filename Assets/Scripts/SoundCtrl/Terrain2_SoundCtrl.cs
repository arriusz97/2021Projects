using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain2_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain2_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain2_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain2_Bgm = false;
        }
    }
}
