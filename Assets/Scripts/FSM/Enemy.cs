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
    [Header("Enemy 변수")]
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private AttackArea m_attackArea;
    private Rigidbody m_rigidbody;
    private BoxCollider m_collider;
    private Animator m_Anim;

    [Header("FSM변수")]
    public eEnemyState m_state;
    public Coroutine m_StateMachine;
    [SerializeField]
    public player m_Target;
    private float m_time;
    private bool m_canAttack;

    public AnimationCurve ac;
    private float playTimer = 0.0f;
    private float playTime = 80.0f;

    [Header("Routine 변수")]
    //물고기들이 움직일 position 저장
    [SerializeField]
    private GameObject[] m_Routine;
    //처음에 0에서 스폰되므로 currentIndex의 초기값을 0으로 지정
    int currentIndex = 0;
    int randomID = 0;

    [Header("Enemy HP 변수")]
    [SerializeField]
    private float m_maxHP;
    private float m_currentHP;

    [Header("Particle system")]
    [SerializeField]
    private ParticleSystem m_BloodEffect;

    /// <summary>
    /// //////////////////blood effect 껐다 켰다 해주기
    /// </summary>

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

    //player에게 맞았을 때 불려질 함수
    public void Hit(float damage)
    {
        //player가 공격 중 일때만 damage 입게
        if (m_Target.m_isAttack)
        {
            m_currentHP -= damage;

            if (m_currentHP <= 0)
            {
                m_state = eEnemyState.DIE;
                StateCheck();
                StopCoroutine(m_StateMachine);
            }
        }
    }

    IEnumerator FSM()
    {
        //To Do
        // Coroutine 돌려서 state check
        while (true)
        {
            //매 프레임마다 state check
            yield return null;
            StateCheck();       
        }

    }

    //current state는 전역변수
    //random으로 0~5까지의 routine position 중 하나 고르는 함수
    public int randomPosition()
    {
        int randomIndex = Random.Range(0, 6);

        //만약 현재 상태가 IDLE 이거나 ATTACK이라면 현재 위치 index return
        if (m_state == eEnemyState.DIE || m_state == eEnemyState.ATTACK)
            return currentIndex;

        if (randomIndex == currentIndex)
        {
            m_state = eEnemyState.IDLE;
        }
        else
        {
            m_state = eEnemyState.SWIM;
        }

        return randomIndex;
    }

    //state check 해서 상태 변환해주는 함수
    //어느 routine position으로 가야할 지 넘겨받음
    public void StateCheck()
    {
        switch (m_state)
        {
            case eEnemyState.IDLE:
                m_Anim.SetBool("IDLE", true);
                m_Anim.SetBool("ATTACK", false);
                m_rigidbody.velocity = Vector3.zero;

                //player가 attackArea에 들어왔다면 state를 attack으로
                if (m_attackArea.m_isAttack)
                {
                    m_state = eEnemyState.ATTACK;
                }
                else
                {
                    //IDLE에서 randomIndex 정해주기
                    randomID = randomPosition();
                }
                break;
            case eEnemyState.SWIM:
                m_Anim.SetBool("SWIM", true);
                m_Anim.SetBool("ATTACK", false);

                //enemy의 위치를 현재에서 random으로 나온 위치로 옮긴다
                if (playTimer <= playTime)
                {
                    transform.position = Vector3.Lerp(this.transform.position, m_Routine[randomID].transform.position, ac.Evaluate(playTimer/playTime));
                    playTimer += Time.deltaTime;
                }

                Vector3 dis = this.transform.position - m_Routine[randomID].transform.position;
                transform.rotation = Quaternion.LookRotation(-dis.normalized);
                float distance = dis.sqrMagnitude;

                //player가 attackArea에 들어왔다면 state를 attack으로
                if (m_attackArea.m_isAttack)
                {
                    m_state = eEnemyState.ATTACK;
                }

                if (distance <= 0.2f)
                {
                    m_state = eEnemyState.IDLE;
                    currentIndex = randomID;
                    playTimer = 0.0f;
                }

                break;
            case eEnemyState.ATTACK:
                if (m_Target != null)
                {
                    Vector3 dir = m_Target.transform.position - transform.position;
                    transform.rotation = Quaternion.LookRotation(dir.normalized);
                    m_Anim.SetBool("ATTACK", true);
                    m_Anim.SetBool("SWIM", false);
                    m_Anim.SetBool("IDLE", false);

                    //player가 enemy의 collider안에 들어왔고, 죽은 상태가 아니라면
                    if (m_canAttack && !m_Target.m_isDead)
                    {
                        m_Target.Hit(20);
                        Debug.Log("Target: " + m_Target + "hit!");
                        m_canAttack = false;                   
                    }
                }
                else if(m_Target == null)
                {
                    m_state = eEnemyState.IDLE;
                }
                //m_rigidbody.velocity = Vector3.zero;
                break;
            case eEnemyState.DIE:
                m_Anim.SetBool("DIE", true);
                m_Anim.SetBool("ATTACK", false);
                m_Anim.SetBool("SWIM", false);
                m_rigidbody.velocity = Vector3.zero;
                break;
        }
    }

    //enemy의 collider box안에 player가 들어왔다면 m_canAttack =true
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canAttack = true;
        }
    }

    //enemy의 collider box안에서 player가 나갔다면 m_canAttack =false
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canAttack = false;
        } 
    }

}
