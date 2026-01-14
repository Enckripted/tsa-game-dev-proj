using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public Inventory inventory { get; set; }

    [SerializeField] private GameObject slotPrefab;

    void Start()
    {
        for (int i = 0; i < inventory.TotalSlots; i++)
        {
            GameObject nUiSlot = Instantiate(slotPrefab, transform);
            ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
            draggable.InventorySlot = inventory.GetSlot(i);
        }
    }
}
