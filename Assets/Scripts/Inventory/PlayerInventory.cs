using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public int inventorySlots;
	[field: SerializeField] public Inventory inventory { get; private set; }
	[field: SerializeField] public double money { get; private set; } = 0;
	[field: SerializeField] public int selectedSlot { get; private set; } = 0;

	public static PlayerInventory instance { get; private set; }

	public void addMoney(double amt)
	{
		money += amt;
	}

	public void removeMoney(double amt)
	{
		if (money - amt < 0) throw new Exception("Attempted to remove " + amt + " money and create a negative balance.");
		money -= amt;
	}

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
