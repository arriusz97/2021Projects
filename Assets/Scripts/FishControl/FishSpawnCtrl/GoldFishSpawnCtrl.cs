using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishSpawnCtrl : MonoBehaviour
{
    //이 script는 Spawn position 오브젝트에 붙어서 spawn을 ctrl 할 것임.

    [SerializeField]
    private GoldFishPool m_goldfishPool;

    [SerializeField]
    private int m_FishSpawnCount; //spawn 되어야 할 물고기 개수
    [SerializeField]
    private int m_FishCurrentCount; //현재 물고기 개수

    private void Start()
    {
        for (m_FishCurrentCount = 0; m_FishCurrentCount < m_FishSpawnCount; m_FishCurrentCount++)
        {
            GoldFishCtrl goldfish = m_goldfishPool.GetFromPool();
            goldfish.transform.position = transform.position;
            goldfish.SpawnPos_Init(this);
        }

        StartCoroutine(SpawnRoutine());
    }

    private Coroutine m_spawnCoroutine;

    private IEnumerator SpawnRoutine()
    {
        //fish가 잡힌 뒤 10초 뒤 re-spawn 
        WaitForSeconds ten = new WaitForSeconds(10);

        while (m_FishCurrentCount < m_FishSpawnCount)
        {
            yield return ten;
            GoldFishCtrl goldfish = m_goldfishPool.GetFromPool();
            goldfish.transform.position = transform.position;
            goldfish.SpawnPos_Init(this);
            m_FishCurrentCount++;
        }
    }

    public void Dead()
    {
        m_FishCurrentCount--;
        if (m_spawnCoroutine == null)
        {
            m_spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }
}
