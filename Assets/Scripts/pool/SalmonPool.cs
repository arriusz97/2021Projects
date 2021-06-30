using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonPool : ObjPool<SalmonCtrl>
{
    public GameObject m_prefabSalmon;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabSalmon.GetComponent<SalmonCtrl>();
    }
}
