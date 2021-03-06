using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public enum MouseButton { Left, Middle, Right }

[RequireComponent(typeof(EventTrigger))]
public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;
    private InventoryObject _previousInventory;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public ItemEffectDatabase theItemEffectDatabase;

    private MouseButton mouseButton;

    //게임 실행시 인터페이스 구성
    public void Awake()
    {
        CreateSlots();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onAfterUpdated += OnSlotUpdate;
        }
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });        
    }

    private void OnEnable()
    {
        theItemEffectDatabase = GameObject.Find("EffectDatabase").GetComponent<ItemEffectDatabase>();
    }

    public void InventoryUpdate()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {            
            OnSlotUpdate(inventory.GetSlots[i]);
        }
    }

    public abstract void CreateSlots();
    //인벤토리 슬롯과 데이터베이스의 아이템 링크
    public void UpdateInventoryLinks()
    {
        int i = 0;
        foreach (var key in slotsOnInterface.Keys.ToList())
        {
            slotsOnInterface[key] = inventory.GetSlots[i];
            i++;
        }
    }
    //아이템 id를 이용해 슬롯을 최신화 한다.
    public void OnSlotUpdate(InventorySlot slot)
    {
        if (slot.item.Id <= -1)
        {
            Debug.Log("item id -1");
            slot.slotDisplay.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot.slotDisplay.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
        else
        {
            Debug.Log("item id" + slot.item.Id);
            slot.slotDisplay.transform.GetChild(0).GetComponent<Image>().sprite = slot.GetItemObject().uiDisplay;
            slot.slotDisplay.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? string.Empty : slot.amount.ToString("n0");
        }
    }
    //인벤토리 업데이트
    public void Update()
    {
        if (_previousInventory != inventory)
        {
            UpdateInventoryLinks();
        }
        _previousInventory = inventory;
    }
    //각 이벤트를 동적으로 생성되는 아이템 슬롯에 부여해준다.
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (!trigger) { Debug.LogWarning("No EventTrigger component found!"); return; }
        var eventTrigger = new EventTrigger.Entry {eventID = type};
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = gameObject.GetComponent<UserInterface>();
        MouseData.slotHoveredOver = obj;
        if (MouseData.interfaceMouseIsOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            if (mouseHoverSlotData.item.Id > -1 && MouseData.interfaceMouseIsOver)
            {
                theItemEffectDatabase.ShowToolTip(mouseHoverSlotData.GetItemObject(), obj.transform.position);
            }
        }       
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        theItemEffectDatabase.HideToolTip();

        MouseData.interfaceMouseIsOver = null;
    }

    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        theItemEffectDatabase.HideToolTip();
    }

    private GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].GetItemObject().uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }

    public void OnDragEnd(GameObject obj)
    {

        Destroy(MouseData.tempItemBeingDragged);

        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    }

    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }
}
