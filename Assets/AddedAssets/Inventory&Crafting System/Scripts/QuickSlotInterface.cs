using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlotInterface : UserInterface
{
    public GameObject[] slots;

    //인벤토리 슬롯을 생성하고 위치, 이벤트 트리거, 아이콘을 생성한다.
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
