using UnityEngine;

public class InventoryUi : MonoBehaviour
{
	public Inventory inventory { get; set; }

	[SerializeField] private GameObject slotPrefab;

	void Start()
	{
		for (int i = 0; i < inventory.totalSlots; i++)
		{
			GameObject nUiSlot = Instantiate(slotPrefab, transform);
			ItemUiDraggable draggable = nUiSlot.GetComponentInChildren<ItemUiDraggable>();
			draggable.inventorySlot = inventory.getSlot(i);
		}
	}
}