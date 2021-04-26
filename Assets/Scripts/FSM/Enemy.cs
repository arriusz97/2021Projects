using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private GameObject m_attackArea;

    Rigidbody m_rigidbody;
    BoxCollider m_collider;
    Animator m_Anim;

    [Header("Enemy HP 변수")]
    [SerializeField]
    private float m_maxHP;
    private float m_currentHP;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<BoxCollider>();
        m_Anim = GetComponent<Animator>();

        m_currentHP = m_maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
