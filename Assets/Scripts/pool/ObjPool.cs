using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    protected T m_Origin;
    protected List<T> m_Pool;

    protected virtual void Awake()
    {
        PoolSetUp();
    }

    public void PoolSetUp()
    {
        m_Pool = new List<T>();
        for(int i=0; i<m_Pool.Count; i++)
        {
            m_Pool = new List<T>();
        }
    }

    //꺼진 object 다시 켜주기
    public T GetFromPool()
    {
        for(int i=0; i<m_Pool.Count; i++)
        {
            if (!m_Pool[i].gameObject.activeInHierarchy)
            {
                m_Pool[i].gameObject.SetActive(true);
                return m_Pool[i];
            }
         //   id = i;
        }
        
        return GetNewObj();
    }

    protected virtual T GetNewObj()
    {
        T newObj = Instantiate(m_Origin);
        m_Pool.Add(newObj);
        return newObj;
    }
}
