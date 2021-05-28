using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjPool<Enemy>
{
    private void Awake()
    {
        m_Origin = Resources.LoadAll<Enemy>("Assets/AddedAssets/Piranha/prefab");
    }
}
