using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonCtrl : MonoBehaviour
{
    [Header("Salmon 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 6f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.5f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 8;
    private bool m_isTurning = false;
    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;

    private SalmonSpawnCtrl m_salmonSpawnCtrl;


    private void Start()
    {
        m_Speed = Random.Range(1f, 7f);
    }

    //init method 만들어서 연결
    public void SpawnPos_Init(SalmonSpawnCtrl spawnCtrl)
    {
       m_salmonSpawnCtrl  = spawnCtrl;
    }

    //player에게 잡혔을 때 불릴 함수
    public void DIE()
    {
        m_salmonSpawnCtrl.Dead();
    }

    private void Update()
    {
        //change target position
        GetTargetPosition();

            if (Random.Range(0, 8) < 1)
                setRotation();

        transform.Translate(0, 0, Time.deltaTime * m_Speed);
    }

    void GetTargetPosition()
    {
        if (Random.Range(1, 80) < 50)
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
        Vector3 direction = m_salmonSpawnCtrl.transform.position - transform.position;
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
