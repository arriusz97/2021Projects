﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public ItemObject _item;
    public string[] part;
    public int[] num;
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    [SerializeField]
    private Tooltip theSlotToolTip;

    private const string HP = "HP", TP = "TP", O2 = "O2";

    //아이템 사용시 발생할 효과 현재 Food만 구현
    public void UseItem(Item item)
    {        
        for (int i = 0; i < itemEffects.Length; i++)
        {
            if (itemEffects[i]._item.data.Name == item.Name)
            {
                if (itemEffects[i]._item.data.itemType == ItemType.Food)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                break;
                            case TP:
                                break;
                            case O2:
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위.");
                                break;
                        }
                        Debug.Log(item.Name + " 을 사용했습니다.");
                    }
                    return;
                }
            }
        }
        Debug.Log("itemEffectDatabase에 일치하는 itemName이 없습니다.");        
    }

    public void ShowToolTip(ItemObject _item, Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }
}