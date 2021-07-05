﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassSpawnCtrl : MonoBehaviour
{
    //이 script는 Spawn position 오브젝트에 붙어서 spawn을 ctrl 할 것임.

    [SerializeField]
    private BassPool m_bassPool;

    [SerializeField]
    private int m_FishSpawnCount; //spawn 되어야 할 물고기 개수
    [SerializeField]
    private int m_FishCurrentCount; //현재 물고기 개수

    private void Start()
    {
        for (m_FishCurrentCount = 0; m_FishCurrentCount < m_FishSpawnCount; m_FishCurrentCount++)
        {
            BassCtrl bass = m_bassPool.GetFromPool();
            bass.transform.position = transform.position;
            bass.SpawnPos_Init(this);
        }

        StartCoroutine(SpawnRoutine());
    }

    private Coroutine m_spawnCoroutine;

    private IEnumerator SpawnRoutine()
    {
        //fish가 잡힌 뒤 15초 뒤 re-spawn 
        WaitForSeconds fifteen = new WaitForSeconds(15);

        while (m_FishCurrentCount < m_FishSpawnCount)
        {
            yield return fifteen;
            BassCtrl bass = m_bassPool.GetFromPool();
            bass.transform.position = transform.position;
            bass.SpawnPos_Init(this);
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
