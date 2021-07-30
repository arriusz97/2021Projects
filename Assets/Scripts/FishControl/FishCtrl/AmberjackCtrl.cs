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
    private int m_Boundary = 20;
    private bool m_isTurning = false;
    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;

    private AmberjackSpawnCtrl m_Amberjack_spawnCtrl;

    [SerializeField]
    private InventoryObject inventory;

    [SerializeField]
    private ItemObject AmberjackItem;

    private void Start()
    {
        m_Speed = Random.Range(1f, 5f);
    }

    //init method 만들어서 연결
    public void SpawnPos_Init(AmberjackSpawnCtrl spawnCtrl)
    {
        m_Amberjack_spawnCtrl = spawnCtrl;
    }

    //player에게 잡혔을 때 불릴 함수
    public void DIE()
    {
        m_Amberjack_spawnCtrl.Dead();
        inventory.AddItem(AmberjackItem.data, 1);
    }

    private void Update()
    {
        //change target position
        GetTargetPosition();

            if (Random.Range(0, 10) < 1)
                setRotation();
        transform.Translate(0, 0, Time.deltaTime * m_Speed);
    }

    void GetTargetPosition()
    {
        if(Random.Range(1, 100) < 50)
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
        Vector3 direction = m_Amberjack_spawnCtrl.transform.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                TurnSpeed() * Time.deltaTime);
        }
    }


    float TurnSpeed()
    {
        return Random.Range(0.1f, m_MaxrotationSpeed);
    }

}

//https://coderzero.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8-%EC%86%8C%EC%8A%A4-Fish-Flock
