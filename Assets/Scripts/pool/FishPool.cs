using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPool : ObjPool<FishSchoolCtrl>
{
    public GameObject[] m_Fish;
    protected override void Awake()
    {
        base.Awake();
      //  m_Origin = m_Fish.GetComponent<FishSchoolCtrl>();
    }


    //Objpool로 가져올 때 fish school 어떻게 ctrl 할 지 고민
}
