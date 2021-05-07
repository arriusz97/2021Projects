using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public bool m_isAttack = false;
    [SerializeField]
    private Enemy m_enemy;
    private void OnEnable()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isAttack = true;
            m_enemy.m_state = eEnemyState.ATTACK;
            m_enemy.StateCheck();            
            Debug.Log("player enter & attack start");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_enemy.m_state = eEnemyState.IDLE;
            m_enemy.m_Target = null;
            Debug.Log("Player exit & attack false");
        }   
    }
}
