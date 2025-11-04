using UnityEngine;

public class DroppedItem : MonoBehaviour
{
	public Item item;

	void OnMouseEnter()
	{
		ItemTooltip._instance.ShowTooltip(item);
	}

	void OnMouseExit()
	{
		ItemTooltip._instance.HideTooltip();
	}
}
