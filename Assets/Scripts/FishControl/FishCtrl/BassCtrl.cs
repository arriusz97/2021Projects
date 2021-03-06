using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassCtrl : MonoBehaviour
{
    [Header("Bass 변수")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed = 5f;
    [SerializeField]
    private float m_MaxrotationSpeed = 0.7f;
    //[SerializeField]
    //private float neighbourDistance;
    [SerializeField]
    private int m_Boundary = 15;
    private bool m_isTurning = false;

    public Vector3 m_targetPosition = Vector3.zero;

    Vector3 averageHeading;
    Vector3 averagePosition;

    private BassSpawnCtrl m_bassSpawnCtrl;

    [SerializeField]
    private InventoryObject inventory;

    [SerializeField]
    private ItemObject bassItem;

    private void Start()
    {
        m_Speed = Random.Range(2f, 5.5f);
    }

    //init method 만들어서 연결
    public void SpawnPos_Init(BassSpawnCtrl spawnCtrl)
    {
        m_bassSpawnCtrl = spawnCtrl;
    }

    //player에게 잡혔을 때 불릴 함수
    public void DIE()
    {
        m_bassSpawnCtrl.Dead();
        inventory.AddItem(bassItem.data, 1);
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
        if (Random.Range(1, 100) < 50)
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
        Vector3 direction = m_bassSpawnCtrl.transform.position - transform.position;
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
