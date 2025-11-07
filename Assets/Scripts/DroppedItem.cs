using UnityEngine;

public class DroppedItem : MonoBehaviour
{
	public Item item;

	void OnMouseDown()
	{
		bool success = InventoryManager.instance.AddItem(item);
		if (success)
		{
			Destroy(gameObject);
			ItemTooltipManager.instance.HideTooltip();
		}
	}

	void OnMouseEnter()
	{
		ItemTooltipManager.instance.ShowTooltip(item);
	}

	void OnMouseExit()
	{
		ItemTooltipManager.instance.HideTooltip();
	}
}
