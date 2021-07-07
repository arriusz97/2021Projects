using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoiSpawnCtrl1 : MonoBehaviour
{
    //이 script는 Spawn position 오브젝트에 붙어서 spawn을 ctrl 할 것임.

    [SerializeField]
    private KoiPool1 m_koiPool1;

    [SerializeField]
    private int m_FishSpawnCount; //spawn 되어야 할 물고기 개수
    [SerializeField]
    private int m_FishCurrentCount; //현재 물고기 개수

    private void Start()
    {
        for (m_FishCurrentCount = 0; m_FishCurrentCount < m_FishSpawnCount; m_FishCurrentCount++)
        {
            KoiCtrl1 koi = m_koiPool1.GetFromPool();
            koi.transform.position = transform.position;
            koi.SpawnPos_Init1(this);
        }

        StartCoroutine(SpawnRoutine());
    }

    private Coroutine m_spawnCoroutine;

    private IEnumerator SpawnRoutine()
    {
        //fish가 잡힌 뒤 12초 뒤 re-spawn 
        WaitForSeconds twelve = new WaitForSeconds(12);

        while (m_FishCurrentCount < m_FishSpawnCount)
        {
            yield return twelve;
            KoiCtrl1 koi = m_koiPool1.GetFromPool();
            koi.transform.position = transform.position;
            koi.SpawnPos_Init1(this);
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
