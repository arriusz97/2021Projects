using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoiCtrl : MonoBehaviour
{
    [Header("Koi 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 6.5f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.8f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 20;
    private bool m_isTurning = false;

    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;

    private KoiSpawnCtrl m_koiSpawnCtrl;

    private void Start()
    {
        m_Speed = Random.Range(3f, 7.5f);
    }

    //init method 만들어서 연결
    public void SpawnPos_Init(KoiSpawnCtrl spawnCtrl)
    {
        m_koiSpawnCtrl = spawnCtrl;
    }

    //player에게 잡혔을 때 불릴 함수
    public void DIE()
    {
       m_koiSpawnCtrl.Dead();
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
        if (Random.Range(1, 90) < 50)
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
        Vector3 direction = (m_koiSpawnCtrl.transform.position - transform.position);

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
