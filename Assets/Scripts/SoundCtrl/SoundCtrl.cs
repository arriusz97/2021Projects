using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    //BGM 
    public AudioSource m_Terrain1_Bgm;
    public AudioSource m_Terrain2_Bgm;
    public AudioSource m_Terrain3_8_Bgm;
    public AudioSource m_Terrain4_Bgm;
    public AudioSource m_Terrain5_6_Bgm;
    public AudioSource m_Terrain7_Bgm;
    public AudioSource m_Island_Bgm;
    public AudioSource m_Island_night_Bgm;

    //boolean 변수 받아오기 위한 ctrl
    [Header("Terrain Sound Ctrl")]
    public Terrain1_SoundCtrl t1_soundCtrl;
    public Terrain2_SoundCtrl t2_soundCtrl;
    public Terrain3_SoundCtrl t3_soundCtrl;
    public Terrain4_SoundCtrl t4_soundCtrl;
    public Terrain5_6_SoundCtrl t5_6_soundCtrl;
    public Terrain7_SoundCtrl t7_soundCtrl;
    public Terrain8_SoundCtrl t8_soundCtrl;
    public SwimTrigger m_swimTrigger;
    public SunController m_sunCtrl;

    [Header("Effect Sound")]
    public AudioSource m_walk_Sand;
    public AudioSource m_run_Sand;
    
    //O2 Gauge에 사용될 효과음
    public AudioSource m_HeartBeat;
    public AudioSource m_Alarm;

    [Header("player script")]
    public player m_playerCtrl;

    [Header("o2 gauge")]
    public TimerController timeCtrl;

    private void Awake()
    {
        m_Island_Bgm.Play();
    }

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

        if (timeCtrl.O2alert)
        {
            Debug.Log("o2 alert true");
            if(!m_HeartBeat.isPlaying && !m_Alarm.isPlaying)
            {
                m_HeartBeat.Play();
                m_Alarm.Play();
            }

        }
        else
        {
            m_HeartBeat.Stop();
            m_Alarm.Stop();
        }

        //물 속에 있지 않다면
        if (!m_swimTrigger.m_isWater)
        {
            //밤이 아니라면
            if (!m_sunCtrl.m_night)
            {
                m_Island_night_Bgm.Stop();

                m_Island_Bgm.volume = 0.3f;
                if (!m_Island_Bgm.isPlaying)
                {
                    m_Island_Bgm.Play();
                }
            }
            else //밤이라면
            {
                m_Island_Bgm.Stop();

                m_Island_night_Bgm.volume = 0.3f;
                if (!m_Island_night_Bgm.isPlaying)
                {
                    m_Island_night_Bgm.Play();
                }
            }
        }
        else
        {
            if (m_Island_Bgm.volume > 0)
            {
                fadeout(m_Island_Bgm, 0.5f);
            }
            m_Island_Bgm.Stop();

            if(m_Island_night_Bgm.volume > 0)
            {
                fadeout(m_Island_night_Bgm, 0.5f);
            }
            m_Island_night_Bgm.Stop();
        }

        //물 속에 있지 않고 
        if (!m_swimTrigger.m_isWater)
        {
           // player가 walk하고 있다면
            if (m_playerCtrl.isMove)
            {
                //playing하고 있지 않다면
                if (!m_walk_Sand.isPlaying)
                {
                    m_walk_Sand.Play();
                }

                //player가 뛰고 있다면
                if (m_playerCtrl.m_isRun)
                {
                    m_walk_Sand.Stop();

                    if (!m_run_Sand.isPlaying)
                    {
                        m_run_Sand.Play();
                    }
                }
                else
                {
                    m_run_Sand.Stop();
                }
            }
        }
        else
        {
            m_walk_Sand.Stop();
            m_run_Sand.Stop();
        }
    }

    public void fadeout(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume -= 0.5f * Time.deltaTime / FadeTime;
    }
}