using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassPool : ObjPool<FishCtrl>
{
    public GameObject m_prefabBass;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabBass.GetComponent<FishCtrl>();
    }
}
