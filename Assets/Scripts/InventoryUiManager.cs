using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    public GameObject slotPrefab;

    void Start()
    {
        for (int i = 0; i < InventoryManager.instance.inventorySlots; i++)
        {
            GameObject nUiSlot = Instantiate(slotPrefab, transform);
            ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
            draggable.inventorySlot = InventoryManager.instance.inventory[i];
        }
    }
}
