using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberjackCtrl : MonoBehaviour
{
    //Todo
    //물고기 특성 -> speed, rotation, etc..
    //행동반경

    [Header("Amberjack 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 5f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.5f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 10;
    private bool m_isTurning = false;
    [SerializeField]
    private FishAreaCtrl m_fishAreaCtrl;
    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;
   

    private void Start()
    {
        m_Speed = Random.Range(2f, 4f);
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
        if(Random.Range(1,10000) < 50)
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

//https://coderzero.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8-%EC%86%8C%EC%8A%A4-Fish-Flock
