using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public Inventory Inventory { get; set; }

    [SerializeField] private GameObject slotPrefab;

    void Start()
    {
        for (int i = 0; i < Inventory.TotalSlots; i++)
        {
            GameObject nUiSlot = Instantiate(slotPrefab, transform);
            ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
            draggable.InventorySlot = Inventory.GetSlot(i);
        }
    }
}
