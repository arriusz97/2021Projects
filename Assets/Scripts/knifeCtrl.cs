using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeCtrl : MonoBehaviour
{
    private BoxCollider m_boxCollider;

    [SerializeField]
    private player m_player;

    private void OnEnable()
    {
        m_boxCollider.GetComponent<BoxCollider>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.m_isAttack)
        {
            //m_boxCollider.gameObject.activeInHierarchy = true;
        }
    }
}
