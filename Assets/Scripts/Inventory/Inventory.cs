using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Inventory
{
	[field: SerializeField] public int totalSlots { get; }
	[field: SerializeField] public int availableSlots { get; private set; }
	[field: SerializeField] private List<InventorySlot> slots;

	public UnityEvent changed;
	private List<bool> prevSlotFillState;

	private void modPrevFillState(int slotIndex)
	{
		if (slots[slotIndex].containsItem == prevSlotFillState[slotIndex]) return;
		if (slots[slotIndex].containsItem) availableSlots -= 1;
		else availableSlots += 1;
		prevSlotFillState[slotIndex] = slots[slotIndex].containsItem;
	}

	public Inventory(int nTotalSlots)
	{
		totalSlots = nTotalSlots;
		availableSlots = nTotalSlots;
		changed = new UnityEvent();
		slots = Enumerable.Range(0, totalSlots).Select(_ => new InventorySlot()).ToList();
		prevSlotFillState = Enumerable.Range(0, totalSlots).Select(_ => false).ToList();

		for (int i = 0; i < totalSlots; i++)
		{
			int index = i; //c# by default passes the int as a reference, so we need to copy it so it doesn't change
			slots[i].changed.AddListener(() => { modPrevFillState(index); changed.Invoke(); });
		}
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
}