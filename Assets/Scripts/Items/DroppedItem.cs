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
			TooltipManager.instance.HideTooltip();
		}
	}

	void OnMouseEnter()
	{
		TooltipManager.instance.ShowTooltip(item);
	}

	void OnMouseExit()
	{
		TooltipManager.instance.HideTooltip();
	}
}
