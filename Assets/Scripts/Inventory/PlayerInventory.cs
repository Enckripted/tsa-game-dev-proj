using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public int inventorySlots;
	[field: SerializeField] public Inventory inventory { get; private set; }
	[field: SerializeField] public int selectedSlot { get; private set; } = 0;

	public static PlayerInventory instance { get; private set; }

	void Awake()
	{
		instance = this;
		instance.inventory = new Inventory(inventorySlots);
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
