using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain7_SoundCtrl : MonoBehaviour
{
    public bool b_Terrain7_Bgm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain7_Bgm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_Terrain7_Bgm = false;
        }
    }
}
