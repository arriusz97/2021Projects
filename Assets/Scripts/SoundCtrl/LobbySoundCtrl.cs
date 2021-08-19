using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySoundCtrl : MonoBehaviour
{
    public AudioSource m_Lobby_Bgm;

    // Start is called before the first frame update
    void Awake()
    {
        m_Lobby_Bgm.Play();
    }

    
}
