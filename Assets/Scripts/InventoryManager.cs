using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public int inventorySlots;
	[field: SerializeField] public List<InventorySlot> inventory { get; private set; } = new();
	[field: SerializeField] public int selectedSlot { get; private set; } = 0;

	public bool addItem(Item item)
	{
		Debug.Log("start");
		for (int slot = 0; slot < inventorySlots; slot++)
		{
			if (inventory[slot].containsItem) continue;
			Debug.Log("insertion");
			inventory[slot].insert(item);
			return true;
		}
		return false;
	}

	public Item popItemInSlot(int slot)
	{
		return inventory[slot].pop();
	}

	public bool itemInSlot(int slot)
	{
		return inventory[slot].containsItem;
	}

	public static InventoryManager instance;

	void Awake()
	{
		instance = this;
		instance.inventory = Enumerable.Repeat(new InventorySlot(), inventorySlots).ToList();
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
