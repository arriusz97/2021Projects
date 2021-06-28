using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amberjack_Pool : ObjPool<FishCtrl>
{
    public GameObject m_prefabAmberjack;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabAmberjack.GetComponent<FishCtrl>();
    }
}
