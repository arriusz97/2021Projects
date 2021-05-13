using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public UserInterface slotScreen;

    private ItemEffectDatabase _theItemEffectDatabase;
    public InventoryObject _inventory;


    private void Start()        //동적으로 생성되는 프리팹이기 때문에 시작 후에 필요한 요소들을 연결한다.
    {        
        slotScreen = gameObject.GetComponentInParent<UserInterface>();
        _theItemEffectDatabase = slotScreen.theItemEffectDatabase;
        _inventory = slotScreen.inventory;
    }

    public void OnPointerClick(PointerEventData eventData)      //PointerEventData를 이용하기 위해 프리팹에 직접 붙여준다.
    {
        InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
        _theItemEffectDatabase.UseItem(mouseHoverSlotData.item);

        if (eventData.button == PointerEventData.InputButton.Right && MouseData.slotHoveredOver != null)    //우클릭
        {
            if (mouseHoverSlotData.item.itemType == ItemType.Food)      //아이템이 음식이라면 수치를 1 감소시킨다.
            {
                _inventory.AddItem(mouseHoverSlotData.item, -1);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && MouseData.slotHoveredOver != null)       //좌클릭
        {
            if (mouseHoverSlotData.item.itemType == ItemType.Recipe)    //레시피라면 제작한다.
            {
                ItemObject io = mouseHoverSlotData.GetItemObject();
                io.Crafting();
            }
        }
    }
}
