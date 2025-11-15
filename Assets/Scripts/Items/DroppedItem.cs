using TMPro;
using UnityEngine;


public class DroppedItem : MonoBehaviour
{
	public TextMeshPro itemName;

	public Item item;

	void Awake()
	{
		itemName.text = item.name;
	}

	void OnMouseDown()
	{
		bool success = InventoryManager.instance.addItem(item);
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
