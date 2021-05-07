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

    public Coroutine m_StateMachine;

    [Header("Enemy HP 변수")]
    [SerializeField]
    private float m_maxHP;
    private float m_currentHP;

    private void OnEnable()
    {
        m_state = eEnemyState.IDLE;
        m_currentHP = m_maxHP;
        m_StateMachine = StartCoroutine(AutoMove());
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

    public void Hit(float damage)
    {
        m_currentHP -= damage;
        
        if(m_currentHP <= 0)
        {
            m_state = eEnemyState.DIE;
            StateCheck();
            StopCoroutine(m_StateMachine);
        }
    }

    public IEnumerator AutoMove()
    {
        while (true)
        {
            yield return null;
            StateCheck();
        }
    }

    public void StateCheck()
    {
        switch (m_state)
        {
            case eEnemyState.IDLE:
                m_Anim.SetBool("IDLE", true);
                m_Anim.SetBool("ATTACK", false);
                m_rigidbody.velocity = Vector3.zero;
                m_state = eEnemyState.SWIM;
                break;
            case eEnemyState.SWIM:
                m_Anim.SetBool("SWIM", true);
                m_Anim.SetBool("ATTACK", false);
                if (Random.Range(0, 2) < 1)
                {
                    m_rigidbody.velocity = Vector3.forward * m_speed;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    m_rigidbody.velocity = Vector3.back * m_speed;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                
                m_state = eEnemyState.IDLE;
                break;
            case eEnemyState.ATTACK:
                m_Anim.SetBool("ATTACK", true);
                m_Anim.SetBool("SWIM", false);

                if (m_Target != null)
                {
                    Vector3 dir = m_Target.transform.position - transform.position;
                    Debug.Log(dir);

                    transform.rotation = Quaternion.LookRotation(dir.normalized);
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
