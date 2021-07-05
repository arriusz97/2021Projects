using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoiPool1 : ObjPool<KoiCtrl>
{
    public GameObject m_prefabKoi2;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabKoi2.GetComponent<KoiCtrl>();
    }
}
