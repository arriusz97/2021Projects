using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberjackSpawnCtrl : MonoBehaviour
{
    //Todo
    //물고기의 spawn ctrl라고 생각하면 될듯
    //생성할 물고기의 pool 가져오기
    //물고기 개체수 관리

    [SerializeField]
    private Amberjack_Pool m_Amberjack_pool;

    [SerializeField]
    private int m_FishSpawnCount; //spawn 되어야 할 물고기 개수
    [SerializeField]
    private int m_FishCurrentCount; //현재 물고기 개수

    private void Start()
    {
        for(m_FishCurrentCount=0; m_FishCurrentCount<m_FishSpawnCount; m_FishCurrentCount++)
        {
            AmberjackCtrl amberjack = m_Amberjack_pool.GetFromPool();
            amberjack.transform.position = transform.position;
            //amberjack.SpawnPos(this);
        }

        StartCoroutine(AmberjackSpawnRoutine());
    }

    private Coroutine m_spawnCoroutine;

    private IEnumerator AmberjackSpawnRoutine()
    {
        //fish가 잡힌 뒤 20초 뒤 re-spawn -> amberjack은 큰 물고기 -> 대기시간 높아야함
        WaitForSeconds twenty = new WaitForSeconds(20);

        while(m_FishCurrentCount < m_FishSpawnCount)
        {
            Debug.Log("Amberjack hunted");
            yield return twenty;
            AmberjackCtrl amberjack = m_Amberjack_pool.GetFromPool();
            amberjack.transform.position = transform.position;
            //amberjack.SpawnPos(this);
            m_FishCurrentCount++;
        }
    }

    public void Dead()
    {
        m_FishCurrentCount--;
        if(m_spawnCoroutine == null)
        {
            m_spawnCoroutine = StartCoroutine(AmberjackSpawnRoutine());
        }
    }


}
