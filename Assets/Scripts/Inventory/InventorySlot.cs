using System;
using UnityEngine;
using UnityEngine.Events;

//Class that allows for the InventorySlot UI component to only control the state of the slot it
//controls
[Serializable]
public class InventorySlot
{
    public readonly UnityEvent Changed;
    [field: SerializeField] public IItem StoredItem { get; private set; }
    [field: SerializeField] public bool ContainsItem { get; private set; }
    [field: SerializeReference] public Inventory TargetInventory { get; set; } //maybe don't serialize
    //TODO: add a custom setter here

    public InventorySlot(Inventory targetInventory)
    {
        Changed = new UnityEvent();
        ContainsItem = false;
        TargetInventory = targetInventory;
    }

    public bool Insert(IItem nItem)
    {
        if (ContainsItem) return false;

        if (nItem != null)
        {
            StoredItem = nItem;
            ContainsItem = true;
            Changed.Invoke();
        }
        return true;
    }

    public IItem Pop()
    {
        IItem temp = StoredItem;
        StoredItem = null;
        ContainsItem = false;
        Changed.Invoke();
        return temp;
    }

    public bool QuickMove()
    {
        if (TargetInventory == null || !TargetInventory.PushItem(StoredItem)) return false;
        Pop();
        return true;
    }
}
