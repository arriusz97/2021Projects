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
    //public eEnemyState m_currentState;
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
        m_StateMachine = StartCoroutine(FSM());
        m_currentHP = m_maxHP;
        m_currentHP = m_maxHP;
        currentIndex = 0;
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
        WaitForSeconds one = new WaitForSeconds(1);
        while (true)
        {
            yield return null;
            //만약 현재 상태가 IDLE 이라면 position 변경
            if (m_state == eEnemyState.IDLE)
            {
                //매 프레임마다 state check
                //yield return null;
                //random index 넘겨받음
                yield return one;
                int randomId = randomPosition();
                Debug.Log("current state: " + m_state + "random position 함수 실행");
                StateCheck(randomId, currentIndex);
            }
        }

    }

    //current state는 전역변수
    //random으로 0~5까지의 routine position 중 하나 고르는 함수
    public int randomPosition()
    {
        int randomIndex = Random.Range(0, 6);
        Debug.Log("random position index : " + randomIndex);

        //만약 현재 상태가 IDLE 이거나 ATTACK이라면 현재 위치 index return
        if (m_state == eEnemyState.DIE || m_state == eEnemyState.ATTACK)
            return currentIndex;

        if (randomIndex == currentIndex)
        {
            m_state = eEnemyState.IDLE;
            Debug.Log("random index == current index");
        }
        else
        {
            m_state = eEnemyState.SWIM;
            Debug.Log("random position에서 state를 swim 으로");
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
                Debug.Log("현재 state는 idle in switch");
                break;
            case eEnemyState.SWIM:
                m_Anim.SetBool("SWIM", true);
                m_Anim.SetBool("ATTACK", false);
                Debug.Log("현재 state는 swim in switch");
                //enemy의 위치를 현재에서 random으로 나온 위치로 옮긴다      
                transform.position = Vector3.Lerp(m_Routine[currentID].transform.position, m_Routine[randomID].transform.position, Time.deltaTime);
                
                //현재 index를 random index로 변경
                currentIndex = randomID;

                if(this.transform.position == m_Routine[randomID].transform.position)
                {
                    m_state = eEnemyState.IDLE;
                }
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
