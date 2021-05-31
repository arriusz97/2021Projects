using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjPool<Enemy>
{
    public GameObject _prefabPiranha;
    protected override void Awake()
    {
        base.Awake();
        m_Origin = _prefabPiranha.GetComponent<Enemy>();
    }
}
