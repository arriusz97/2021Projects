using System.Collections;
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

    [SerializeField]
    private TimerController timer;

    private AudioSource pickup;

    private const string HP = "HP", TP = "TP", O2 = "O2";

    [SerializeField]
    private ActionController AC;

    [SerializeField]
    private SunController SC;

    private void Awake()
    {
        pickup = GetComponent<AudioSource>();
    }

    private void Start()
    {
        theSlotToolTip = GameObject.Find("GUI").transform.Find("UI").transform.Find("Tooltip").GetComponent<Tooltip>();
    }

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
                                timer.UpdateTimer(0, itemEffects[i].num[j]);
                                break;
                            case TP:
                                timer.UpdateTimer(1, itemEffects[i].num[j]);
                                break;
                            case O2:
                                timer.UpdateTimer(2, itemEffects[i].num[j]);
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위.");
                                break;
                        }
                        Debug.Log(item.Name + " 을 사용했습니다.");
                    }                    
                    return;
                }
                else if (itemEffects[i]._item.data.itemType == ItemType.Tool)
                {
                    if(item.Name == "Flippers")
                    {
                        AC.SwimSpeedUpgrade();
                    }
                    else if(item.Name == "O2")
                    {
                        AC.O2gaugeUpgrade();
                    }
                    else if(item.Name == "PLB")
                    {
                        SC.RescueCall();
                        Debug.Log("구조신호 발생");
                    }
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

    public void pickupSound()
    {
        pickup.Play();
    }
}