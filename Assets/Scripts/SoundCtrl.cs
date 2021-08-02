using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    //BGM Ctrl
    public AudioSource m_Terrain1_Bgm;
    public AudioSource m_Terrain2_Bgm;
    public AudioSource m_Terrain3_8_Bgm;
    public AudioSource m_Terrain4_Bgm;
    public AudioSource m_Terrain5_6_Bgm;
    public AudioSource m_Terrain7_Bgm;
    public AudioSource m_Island_Bgm;

    private void Awake()
    {
        m_Island_Bgm.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //https://ansohxxn.github.io/unity%20lesson%202/ch9-1/
}
