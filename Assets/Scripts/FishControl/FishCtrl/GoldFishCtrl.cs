using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishCtrl : MonoBehaviour
{
    [Header("GoldFish 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 7f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.9f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 15;
    private bool m_isTurning = false;

    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;

    private GoldFishSpawnCtrl m_goldfishSpawnCtrl;


    private void Start()
    {
        m_Speed = Random.Range(1f, 8f);
    }

    //init method 만들어서 연결
    public void SpawnPos_Init(GoldFishSpawnCtrl spawnCtrl)
    {
        m_goldfishSpawnCtrl = spawnCtrl;
    }

    private void Update()
    {
        //change target position
        GetTargetPosition();

            if (Random.Range(0, 5) < 1)
                setRotation();

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
        Vector3 direction = m_goldfishSpawnCtrl.transform.position - transform.position;
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
