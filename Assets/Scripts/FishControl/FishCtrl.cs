using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCtrl : MonoBehaviour
{
    //Todo
    //3. random path 설정해주기 -> wander function 생성
    //4. avoid obstacle -> 더 공부해보기 // obstacle 만나면 좌/우로 방향 틀어서 다시 wander

    [Header("Fish 변수")]
    [SerializeField]
    private float m_SmallFish_Speed;
    [SerializeField]
    private float m_BigFish_Speed;


    [Header("Roaming Area 변수")]
    [SerializeField]
    private float m_AreaWidth;
    [SerializeField]
    private float m_AreaDepth;
    [SerializeField]
    private float m_AreaHeight;

    public void OnDrawGizmos()
    {
       // if (!Application.isPlaying && _posBuffer != transform.position + _posOffset) _posBuffer = transform.position + _posOffset;
       // Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(_posBuffer, new Vector3(_spawnSphere * 2, _spawnSphereHeight * 2, _spawnSphereDepth * 2));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3((m_AreaWidth * 2), (m_AreaHeight * 2) + m_AreaHeight * 2, (m_AreaDepth * 2) + m_AreaDepth * 2));
    }
}
