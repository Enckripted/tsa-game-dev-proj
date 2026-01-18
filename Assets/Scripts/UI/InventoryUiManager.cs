using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    public GameObject SlotPrefab;

    void Start()
    {
        for (int i = 0; i < Player.PlayerInventory.TotalSlots; i++)
        {
            GameObject nUiSlot = Instantiate(SlotPrefab, transform);
            ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
            draggable.InventorySlot = Player.PlayerInventory.GetSlot(i);
        }
    }
}
