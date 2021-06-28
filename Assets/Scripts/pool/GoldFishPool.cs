using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishPool : ObjPool<FishCtrl>
{
    public GameObject m_prefabGoldFish;

    protected override void Awake()
    {
        base.Awake();
        m_Origin = m_prefabGoldFish.GetComponent<FishCtrl>();
    }
}
