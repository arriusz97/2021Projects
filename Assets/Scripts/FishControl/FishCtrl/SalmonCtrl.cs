using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonCtrl : MonoBehaviour
{
    [Header("Salmon 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 7f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.5f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 8;
    private bool m_isTurning = false;
    [SerializeField]
    private FishAreaCtrl m_fishAreaCtrl;
    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;


    private void Start()
    {
        m_Speed = Random.Range(3f, 7f);
    }

    private void Update()
    {
        //change target position
        GetTargetPosition();

        //fish area ctrl에서 area 벗어나면 m_isTurning = true, 들어오면 false
        if (m_fishAreaCtrl.m_isTurning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), TurnSpeed() * Time.deltaTime);
            m_Speed = Random.Range(0.5f, m_MaxSpeed);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
                setRotation();
        }
        transform.Translate(0, 0, Time.deltaTime * m_Speed);
    }

    void GetTargetPosition()
    {
        if (Random.Range(1, 1000) < 50)
        {
            m_targetPosition = new Vector3(
                Random.Range(-m_Boundary, m_Boundary),
                Random.Range(-m_Boundary, m_Boundary),
                Random.Range(-m_Boundary, m_Boundary)
                );
        }
    }

    void setRotation()
    {
        Vector3 direction = m_fishAreaCtrl.transform.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                TurnSpeed() * Time.deltaTime);
        }
    }


    float TurnSpeed()
    {
        return Random.Range(0.2f, m_MaxrotationSpeed);
    }
}
