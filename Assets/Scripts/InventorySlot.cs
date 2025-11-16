using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventorySlot
{
    public readonly UnityEvent changed;
    [field: SerializeField] public Item item { get; private set; }
    [field: SerializeField] public bool containsItem { get; private set; }

    public InventorySlot()
    {
        changed = new UnityEvent();
        containsItem = false;
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
}
