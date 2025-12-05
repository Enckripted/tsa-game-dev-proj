using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventorySlot
{
    public readonly UnityEvent changed;
    [field: SerializeField] public Item item { get; private set; }
    [field: SerializeField] public bool containsItem { get; private set; }
    [field: SerializeReference] public Inventory targetInventory { get; set; } //maybe don't serialize
    //TODO: add a custom setter here

    public InventorySlot(Inventory targetInventory)
    {
        this.changed = new UnityEvent();
        this.containsItem = false;
        this.targetInventory = targetInventory;
    }

    public bool insert(Item nItem)
    {
        if (containsItem) return false;

        if (nItem != null)
        {
            item = nItem;
            containsItem = true;
            changed.Invoke();
        }
        return true;
    }

    public Item pop()
    {
        Item temp = item;
        item = null;
        containsItem = false;
        changed.Invoke();
        return temp;
    }

    public bool quickMove()
    {
        if (targetInventory == null || !targetInventory.pushItem(item)) return false;
        pop();
        return true;
    }
}
