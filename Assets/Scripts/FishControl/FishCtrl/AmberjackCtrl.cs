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
    [SerializeField]
    private float neighbourDistance;
    private bool m_isTurning = false;

    Vector3 averageHeading;
    Vector3 averagePosition;

    //boundary를 x, y 지정해서 사용할 수도 있음
    [Header("Roaming Area 변수")]
    [SerializeField]
    private float m_AreaWidth;
    [SerializeField]
    private float m_AreaDepth;
    [SerializeField]
    private float m_AreaHeight;
   

    private void Start()
    {
        m_Speed = Random.Range(2f, 4f);
    }

    float TurnSpeed()
    {
        return Random.Range(0.2f, m_MaxrotationSpeed);
    }

    private void Update()
    {
        GetIsTurning();

        if (m_isTurning)
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

    void GetIsTurning()
    {
        //boundary에 부딪치면 turning true
        //else false
    }

    void setRotation()
    {
        //그룹 사이즈에 따라서 rotation 시킴 -> 나는 각각의 fish에 붙어서 제어하는 ctrl
        //흠.. 어떻게 rotation을 시킬까...
        //random.range해서 몇 나오면 rotation 시키고 아니면 그대로 직진하게 하고 할까
    }

    

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3((m_AreaWidth * 2), (m_AreaHeight * 2) + m_AreaHeight * 2, (m_AreaDepth * 2) + m_AreaDepth * 2));
    }
}

//https://coderzero.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8-%EC%86%8C%EC%8A%A4-Fish-Flock
