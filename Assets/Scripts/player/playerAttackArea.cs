using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackArea : MonoBehaviour
{
    [SerializeField]
    private player m_player;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_player.AttackTarget(other.gameObject);
            Debug.Log("Attack target : " + other.gameObject.name);
        }
    }
}
