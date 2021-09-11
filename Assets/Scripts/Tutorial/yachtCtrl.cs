using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yachtCtrl : MonoBehaviour
{
    [Header("yacht 속성")]
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private yachtDrivingSit m_drivingSitCtrl;

    Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


}
