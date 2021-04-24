using System;
using UnityEngine;

[Serializable]
public class ItemBuff : IModifiers
{
    public Attributes stat;
    public int value;
    [SerializeField]
    private int min;
    public int Min => min;
    [SerializeField]
    private int max;
    public int Max => max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateField();
    }

    public void AddValue(ref int v)
    {
        v += value;
    }
    //아이템의 속성값을 생성한다.
    public void GenerateField()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
