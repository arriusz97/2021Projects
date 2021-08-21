using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public eInteractionType InteractionType;

    [SerializeField]
    private InventoryObject objectInventory;

    [SerializeField]
    private TimerController timer;

    private int interactionTime;

    public void ActionClockOn(int actiontime)
    {
        interactionTime = actiontime;
        timer.ActionClockOn(interactionTime);
    }

    public void ItemRoot(InventoryObject playerInventory)
    {
        StartCoroutine(Rooting(playerInventory));
    }
    
    IEnumerator Rooting(InventoryObject playerInventory)
    {
        yield return new WaitForSeconds(interactionTime);

        InventorySlot[] slots = objectInventory.GetSlots;
        foreach (InventorySlot slot in slots)
        {
            playerInventory.AddItem(slot.item, slot.amount);
        }

        Destroy(gameObject, 1);
    }
}