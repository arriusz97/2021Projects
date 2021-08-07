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

    [Header("Terrain Sound Ctrl")]
    public Terrain1_SoundCtrl t1_soundCtrl;
    public Terrain2_SoundCtrl t2_soundCtrl;
    public Terrain3_SoundCtrl t3_soundCtrl;
    public Terrain4_SoundCtrl t4_soundCtrl;
    public Terrain5_6_SoundCtrl t5_6_soundCtrl;
    public Terrain7_SoundCtrl t7_soundCtrl;
    public Terrain8_SoundCtrl t8_soundCtrl;
    public SwimTrigger m_swimTrigger;

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
        //terrain1로 들어갔으면 terrain1 bgm 틀기
        if (t1_soundCtrl.b_Terrain1_Bgm)
        {
            m_Terrain1_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain1_Bgm.isPlaying)
            {
                m_Terrain1_Bgm.Play();
            }
        }
        else
        {
            if(m_Terrain1_Bgm.volume > 0)
            {
                fadeout(m_Terrain1_Bgm, 0.5f);
            }
            m_Terrain1_Bgm.Stop();
        }

        if (t2_soundCtrl.b_Terrain2_Bgm)
        {
            m_Terrain2_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain2_Bgm.isPlaying)
            {
                m_Terrain2_Bgm.Play();
            }
        }
        else
        {
            if (m_Terrain2_Bgm.volume > 0)
            {
                fadeout(m_Terrain2_Bgm, 0.5f);
            }
            m_Terrain2_Bgm.Stop();
        }

        //piranha area
        if (t3_soundCtrl.b_Terrain3_Bgm || t8_soundCtrl.b_Terrain8_Bgm)
        {
            m_Terrain3_8_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain3_8_Bgm.isPlaying)
            {
                m_Terrain3_8_Bgm.Play();
            }
        }
        else
        {
            if (m_Terrain3_8_Bgm.volume > 0)
            {
                fadeout(m_Terrain3_8_Bgm, 0.5f);
            }

            m_Terrain3_8_Bgm.Stop();
        }

        if (t4_soundCtrl.b_Terrain4_Bgm)
        {
            m_Terrain4_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain4_Bgm.isPlaying)
            {
                m_Terrain4_Bgm.Play();
            }
        }
        else
        {
            if (m_Terrain4_Bgm.volume > 0)
            {
                fadeout(m_Terrain4_Bgm, 0.5f);
            }
            m_Terrain4_Bgm.Stop();
        }

        if (t5_6_soundCtrl.b_Terrain5_6_Bgm)
        {
            m_Terrain5_6_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain5_6_Bgm.isPlaying)
            {
                m_Terrain5_6_Bgm.Play();
            }
        }
        else
        {
            if (m_Terrain5_6_Bgm.volume > 0)
            {
                fadeout(m_Terrain5_6_Bgm, 0.5f);
            }
            m_Terrain5_6_Bgm.Stop();
        }

        if (t7_soundCtrl.b_Terrain7_Bgm)
        {
            m_Terrain7_Bgm.volume = 0.3f;
            //play중이 아니라면
            if (!m_Terrain7_Bgm.isPlaying)
            {
                m_Terrain7_Bgm.Play();
            }
        }
        else
        {
            if (m_Terrain7_Bgm.volume > 0)
            {
                fadeout(m_Terrain7_Bgm, 0.5f);
            }
            m_Terrain7_Bgm.Stop();
        }

        //물 속에 있지 않다면
        if (!m_swimTrigger.m_isWater)
        {
            m_Island_Bgm.volume = 0.3f;
            if (!m_Island_Bgm.isPlaying)
            {
                m_Island_Bgm.Play();
            }
        }
        else
        {
            if (m_Island_Bgm.volume > 0)
            {
                fadeout(m_Island_Bgm, 0.5f);
            }
            m_Island_Bgm.Stop();
        }
    }

    //public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    //{
    //    float startVolume = audioSource.volume;

    //    while (audioSource.volume > 0)
    //    {
    //        audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

    //        yield return null;
    //    }

    //    audioSource.Stop();
    //    audioSource.volume = startVolume;

    public void fadeout(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume -= 0.3f * Time.deltaTime / FadeTime;
    }
 //fade out 수정
}