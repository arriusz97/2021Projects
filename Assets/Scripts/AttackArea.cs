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
            m_enemy.m_Target = other.gameObject;
            m_isAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_enemy.m_Target = null;
            m_isAttack = false;
        }   
    }
}
