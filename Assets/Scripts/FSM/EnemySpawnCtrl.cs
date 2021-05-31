using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCtrl : MonoBehaviour
{
    //이 script는 Spawn position 오브젝트에 붙어서 spawn을 ctrl 할 것임. 

    [SerializeField]
    private EnemyPool m_EnemyPool;
    
    [SerializeField]
    private int m_EnemySpawnCount; //spawn 되어야 할 enemy #
    private int m_EnemyCurrentCount; //현재 enemy #

    [SerializeField]
    private GameObject[] m_Routine;


    // Start is called before the first frame update
    void Start()
    {
        //스폰하려는 enemy수 만큼 spawn
      for(m_EnemyCurrentCount=0; m_EnemyCurrentCount < m_EnemySpawnCount; m_EnemyCurrentCount++)
        {
            Enemy enemy = m_EnemyPool.GetFromPool();
            enemy.transform.position = transform.position;
            enemy.SpawnPos_Init(this);
            enemy.Routine_Init(m_Routine);
        }

        // start coroutine
        StartCoroutine(EnemySpawnRoutine());
    }

    private Coroutine m_SpawnRoutine;

    private IEnumerator EnemySpawnRoutine()
    {
        //enemy가 죽은 뒤 10초 후 re-spawn
        WaitForSeconds ten = new WaitForSeconds(10);

        //만약 enemy가 죽었다면
        while (m_EnemyCurrentCount < m_EnemySpawnCount)
        {
            Debug.Log("Enemy died");
            yield return ten;
            Enemy enemy = m_EnemyPool.GetFromPool();
            enemy.transform.position = transform.position;
            enemy.SpawnPos_Init(this);
            enemy.Routine_Init(m_Routine);
            m_EnemyCurrentCount++;
        }

    }

    //enemy Script에서 die시 불러줘서 current count 하나줄이고, getfrompool 할 수 있게 함
    public void Dead()
    {
        m_EnemyCurrentCount--;
        if (m_SpawnRoutine == null)
        {
            m_SpawnRoutine = StartCoroutine(EnemySpawnRoutine());
        }
    }
}
