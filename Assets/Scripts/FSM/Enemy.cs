using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyState
{
    IDLE,
    SWIM,
    ATTACK,
    DIE
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private AttackArea m_attackArea;

    private Rigidbody m_rigidbody;
    private BoxCollider m_collider;
    private Animator m_Anim;
    public eEnemyState m_state;
    [SerializeField]
    public player m_Target;
    private float m_time;

    public Coroutine m_StateMachine;

    [Header("Routine 변수")]
    //물고기들이 움직일 position 저장
    [SerializeField]
    private GameObject[] m_Routine;

    //처음에 0에서 스폰되므로 currentIndex의 초기값을 0으로 지정
    int currentIndex = 0;

    [Header("Enemy HP 변수")]
    [SerializeField]
    private float m_maxHP;
    private float m_currentHP;

    private void OnEnable()
    {
        m_state = eEnemyState.IDLE;
        m_currentHP = m_maxHP;
        m_currentHP = m_maxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<BoxCollider>();
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FSM()
    {
        //To Do
        // Coroutine 돌려서 state check
        yield return null;
        while (true)
        {
            if(m_state == eEnemyState.IDLE)
            {
                randomPosition();
            }
        }

    }

    //current state는 전역변수
    //random으로 0~5까지의 routine position 중 하나 고르는 함수
    public int randomPosition()
    {
        int randomIndex = Random.Range(0, 6);

        //Todo
        //if(현재 상태가 DIE 또는 ATTACK)
        // return currentState;

        //if(random == current)
        // 현재상태를 IDLE
        //else
        // 현재상태를 SWIM

        //return randomIndex


        if (randomIndex == currentIndex)
        {
            m_state = eEnemyState.IDLE;
        }
        else
        {
            if (m_state != eEnemyState.DIE || m_state != eEnemyState.ATTACK)
                return 0;
            m_state = eEnemyState.SWIM;
        }

        return randomIndex;
    }

    //state check 해서 상태 변환해주는 함수
    //어느 routine position으로 가야할 지 넘겨받음
    public void StateCheck(int randomID, int currentID)
    {
        switch (m_state)
        {
            case eEnemyState.IDLE:
                m_Anim.SetBool("IDLE", true);
                m_Anim.SetBool("ATTACK", false);
                m_rigidbody.velocity = Vector3.zero;
                break;
            case eEnemyState.SWIM:

                //Todo
                //current 위치 -> random 위치로 변경
                //current 위치 = random 위치
                //if(this.transform.position == random 위치)
                //현재 state를 IDLE로 변경
                m_Anim.SetBool("SWIM", true);
                m_Anim.SetBool("ATTACK", false);
                
                //enemy의 위치를 현재에서 random으로 나온 위치로 옮긴다      
                transform.position = Vector3.Lerp(m_Routine[currentID].transform.position, m_Routine[randomID].transform.position, Time.deltaTime * 0.2f);
                
                //현재 index를 random index로 변경
                currentIndex = randomID;
                break;
            case eEnemyState.ATTACK:
                m_Anim.SetBool("ATTACK", true);
                m_Anim.SetBool("SWIM", false);
                if (m_Target != null)
                {
                    Vector3 dir2 = m_Target.transform.position - transform.position;
                    Debug.Log(dir2);
                    transform.rotation = Quaternion.LookRotation(dir2.normalized);
                    //Quaternion lookRotation = Quaternion.LookRotation(dir);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.1f);
                }
                m_rigidbody.velocity = Vector3.zero;
                break;
            case eEnemyState.DIE:
                m_Anim.SetBool("DIE", true);
                m_Anim.SetBool("ATTACK", false);
                m_Anim.SetBool("SWIM", false);
                m_rigidbody.velocity = Vector3.zero;
                break;
        }
    }

}
