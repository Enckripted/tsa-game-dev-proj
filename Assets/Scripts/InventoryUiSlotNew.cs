using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUiSlotNew : MonoBehaviour, IDropHandler
{
    public InventorySlot inventorySlot;
    public ItemUiDraggable inventoryDraggable { get; private set; }
    public bool hasDraggable { get; private set; }

    private void swapDraggables(ItemUiDraggable draggable)
    {
        InventoryUiSlotNew otherSlot = draggable.uiSlot.GetComponent<InventoryUiSlotNew>();

        InventorySlot tempSlot = otherSlot.inventorySlot;


    }

    public void Awake()
    {
        hasDraggable = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped");
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject == null) return;

        ItemUiDraggable draggable = draggedObject.Get
    }
}
