﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoiPool : ObjPool<FishCtrl>
{
    public GameObject m_prefabKoi;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabKoi.GetComponent<FishCtrl>();
    }
}
