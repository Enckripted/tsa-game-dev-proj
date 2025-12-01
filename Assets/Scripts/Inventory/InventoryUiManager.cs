using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    public GameObject slotPrefab;

    void Start()
    {
        for (int i = 0; i < PlayerInventory.instance.inventorySlots; i++)
        {
            GameObject nUiSlot = Instantiate(slotPrefab, transform);
            ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
            draggable.inventorySlot = PlayerInventory.instance.inventory.getSlot(i);
        }
    }
}
