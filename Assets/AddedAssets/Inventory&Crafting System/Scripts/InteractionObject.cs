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

    [SerializeField]
    protected int interactionTime = 3;

    [SerializeField]
    protected bool interactionReady = true;

    public void ActionClockOn()
    {
        timer.ActionClockOn(interactionTime);
    }

    public void ItemRoot(InventoryObject playerInventory)
    {
        interactionReady = false;
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
    
    public void Dismantle()
    {
        interactionReady = false;
        Destroy(gameObject);
    }

    public bool getReady()
    {
        return interactionReady;
    }
}