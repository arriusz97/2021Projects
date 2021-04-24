using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[40];

    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].item = new Item();
            Slots[i].amount = 0;
        }
    }
    //아이템 오브젝트 또는 ID를 통해 인벤토리 내의 아이템이 있는지 검사한다.
    public bool ContainsItem(ItemObject itemObject)
    {
        return Array.Find(Slots, i => i.item.Id == itemObject.data.Id) != null;
    }
    
    public bool ContainsItem(int id)
    {
        return Slots.FirstOrDefault(i => i.item.Id == id) != null;
    }
}