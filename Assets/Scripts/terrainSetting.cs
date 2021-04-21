using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainSetting : MonoBehaviour
{
    public float distance;
    public Terrain m_terrain;

    // Start is called before the first frame update
    void Start()
    {
        m_terrain.treeDistance = distance;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
