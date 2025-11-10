using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditorInternal;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public int inventorySlots;
	[field: SerializeField] public List<Item> inventory { get; private set; } = new();

	[field: SerializeField] public int selectedSlot { get; private set; } = 0;

	public bool AddItem(Item item)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i] != null)
			{
				continue;
			}
			inventory[i] = item;
			return true;
		}

		if (inventory.Count < inventorySlots)
		{
			inventory.Add(item);
			return true;
		}

		return false;
	}

	public void RemoveItemAtSlot(int slot)
	{
		if (slot >= inventorySlots)
		{
			Debug.LogWarning("Attempt to remove item at slot " + slot);
			return;
		}

		inventory[slot] = null;
	}

	public static InventoryManager instance;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		for (int i = 0; i < inventorySlots; i++)
		{
			if (Input.GetKey(KeyCode.Alpha1 + i))
			{
				selectedSlot = i;
				break;
			}
		}
	}
}
