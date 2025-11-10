using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventoryUiSlot> slots;

    void Start()
    {
        for (int i = 0; i < InventoryManager.instance.inventorySlots; i++)
        {
            slots.Add(Instantiate(slotPrefab, transform).GetComponent<InventoryUiSlot>());
        }
    }

    void Update()
    {
        for (int i = 0; i < InventoryManager.instance.inventorySlots; i++)
        {
            slots[i].selected = InventoryManager.instance.selectedSlot == i;

            if (InventoryManager.instance.inventory.Count <= i)
            {
                slots[i].itemInSlot = null;
                continue;
            }
            slots[i].itemInSlot = InventoryManager.instance.inventory[i];
        }
    }
}
