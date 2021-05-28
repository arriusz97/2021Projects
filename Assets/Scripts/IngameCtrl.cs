using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameCtrl : MonoBehaviour
{
    //Todo
    //Enemy ObjPool로 spawn하기
    //Enemy routine 넘겨주기  -> terrain 3인지 4인지 구분해서
    //piranha 개체 수 5개 유지하기 -> 각 terrain 당 유지해야함 -> terrain 당 조절하는 script 배치?

     //terrain3,4 니까 spawnpos 두 곳 -> 각 terrain routine id = 0 를 spawn pos로 지정
    [SerializeField]
    private Transform[] m_EnemySpawnPos;
    [SerializeField]
    private EnemyPool m_EnemyPool;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
