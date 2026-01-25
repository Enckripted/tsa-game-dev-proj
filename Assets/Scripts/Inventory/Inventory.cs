using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//TODO: Maybe support having inventories of a single item type? Could be useful
//in cases like the Applier machine (adds gems to an item) to prevent confusion,
//but definetely not enough time to do this now.

//Inventory with support for adding and removing items, counting available items, and shift clicking
//items into a target inventory. IEnumerable means that it can be iterated through with a foreach
//loop.
[Serializable]
public class Inventory : IEnumerable
{
    [field: SerializeField] public int TotalSlots { get; private set; }
    [field: SerializeField] public int AvailableSlots { get; private set; }
    public int UsedSlots { get => TotalSlots - AvailableSlots; }
    [field: SerializeField] public bool CanInsert { get; private set; }
    public Inventory TargetInventory { get => _targetInventory; set { _targetInventory = value; UpdateTargetInventory(); } }
    [field: SerializeField] public List<InventorySlot> Slots { get; private set; }

    private Inventory _targetInventory;

    public UnityEvent Changed;
    private readonly List<bool> prevSlotFillState;

    //utility function for keeping track of how many available slots are left in the inventory
    //which is used in stuff like machines
    private void ModPrevFillState(int slotIndex)
    {
        if (Slots[slotIndex].ContainsItem == prevSlotFillState[slotIndex]) return;
        if (Slots[slotIndex].ContainsItem) AvailableSlots -= 1;
        else AvailableSlots += 1;
        prevSlotFillState[slotIndex] = Slots[slotIndex].ContainsItem;
    }

    public Inventory(int totalSlots, bool canInsert = true, Inventory targetInventory = null)
    {
        TotalSlots = totalSlots;
        AvailableSlots = totalSlots;
        CanInsert = canInsert;
        Changed = new UnityEvent();

        Slots = Enumerable.Range(0, totalSlots).Select(_ => new InventorySlot(canInsert, _targetInventory)).ToList();
        this.prevSlotFillState = Enumerable.Repeat(false, totalSlots).ToList();

        TargetInventory = targetInventory;

        for (int i = 0; i < totalSlots; i++)
        {
            int index = i; //c# by default passes the int as a reference, so we need to copy it so it doesn't change
            Slots[i].Changed.AddListener(() => { ModPrevFillState(index); Changed.Invoke(); });
        }
    }

    public IEnumerator GetEnumerator()
    {
        return Slots.GetEnumerator();
    }

    public bool PushItem(IItem item)
    {
        for (int i = 0; i < TotalSlots; i++)
            if (Slots[i].Insert(item)) return true;
        return false;
    }

    public void RemoveItemFromSlot(int slotIndex)
    {
        Slots[slotIndex].Pop();
    }

    public IItem ItemInSlot(int slotIndex)
    {
        if (!SlotContainsItem(slotIndex)) throw new Exception("Attempted to see nonexistent item in slot " + slotIndex);
        return Slots[slotIndex].StoredItem;
    }

    public bool SlotContainsItem(int slotIndex)
    {
        return Slots[slotIndex].ContainsItem;
    }

    public InventorySlot GetSlot(int slotIndex)
    {
        return Slots[slotIndex];
    }

    private void UpdateTargetInventory()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.TargetInventory = _targetInventory;
        }
    }
}
