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


    private void Start()
    {        
        slotScreen = gameObject.GetComponentInParent<UserInterface>();
        _theItemEffectDatabase = slotScreen.theItemEffectDatabase;
        _inventory = slotScreen.inventory;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
        _theItemEffectDatabase.UseItem(mouseHoverSlotData.item);

        if (eventData.button == PointerEventData.InputButton.Right && MouseData.slotHoveredOver != null)
        {
            if (mouseHoverSlotData.item.itemType == ItemType.Food)
            {
                _inventory.AddItem(mouseHoverSlotData.item, -1);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && MouseData.slotHoveredOver != null)
        {
            if (mouseHoverSlotData.item.itemType == ItemType.Recipe)
            {
                ItemObject io = mouseHoverSlotData.GetItemObject();
                io.Crafting();
            }
        }
    }
}
