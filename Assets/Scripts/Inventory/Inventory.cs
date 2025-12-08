using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Inventory : IEnumerable
{
	[field: SerializeField] public int totalSlots { get; }
	[field: SerializeField] public int availableSlots { get; private set; }
	public Inventory targetInventory { get => _targetInventory; set { _targetInventory = value; updateTargetInventory(); } }
	[field: SerializeField] public List<InventorySlot> slots { get; private set; }

	private Inventory _targetInventory;

	public UnityEvent changed;
	private List<bool> prevSlotFillState;

	private void modPrevFillState(int slotIndex)
	{
		if (slots[slotIndex].containsItem == prevSlotFillState[slotIndex]) return;
		if (slots[slotIndex].containsItem) availableSlots -= 1;
		else availableSlots += 1;
		prevSlotFillState[slotIndex] = slots[slotIndex].containsItem;
	}

	public Inventory(int nTotalSlots, Inventory targetInventory = null)
	{
		this.totalSlots = nTotalSlots;
		this.availableSlots = nTotalSlots;
		this.changed = new UnityEvent();

		this.slots = Enumerable.Range(0, totalSlots).Select(_ => new InventorySlot(_targetInventory)).ToList();
		this.prevSlotFillState = Enumerable.Repeat(false, totalSlots).ToList();

		this.targetInventory = targetInventory;

		for (int i = 0; i < totalSlots; i++)
		{
			int index = i; //c# by default passes the int as a reference, so we need to copy it so it doesn't change
			this.slots[i].changed.AddListener(() => { modPrevFillState(index); changed.Invoke(); });
		}
	}

	public IEnumerator GetEnumerator()
	{
		return (IEnumerator)slots;
	}

	public bool pushItem(Item item)
	{
		for (int i = 0; i < totalSlots; i++)
			if (slots[i].insert(item)) return true;
		return false;
	}

	public void removeItemFromSlot(int slotIndex)
	{
		slots[slotIndex].pop();
	}

	public Item itemInSlot(int slotIndex)
	{
		if (!slotContainsItem(slotIndex)) throw new Exception("Attempted to see nonexistent item in slot " + slotIndex);
		return slots[slotIndex].item;
	}

	public bool slotContainsItem(int slotIndex)
	{
		return slots[slotIndex].containsItem;
	}

	public InventorySlot getSlot(int slotIndex)
	{
		return slots[slotIndex];
	}

	private void updateTargetInventory()
	{
		foreach (InventorySlot slot in slots)
		{
			slot.targetInventory = _targetInventory;
		}
	}
}