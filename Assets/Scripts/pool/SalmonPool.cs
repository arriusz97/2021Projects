using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonPool : ObjPool<FishCtrl>
{
    public GameObject m_prefabSalmon;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabSalmon.GetComponent<FishCtrl>();
    }
}
