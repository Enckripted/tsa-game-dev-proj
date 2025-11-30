using TMPro;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : MonoBehaviour
{
	public TextMeshPro itemName;

	public Item item;

	private Interactable interactable;

	void pickupItem()
	{
		bool success = PlayerInventory.instance.inventory.pushItem(item);
		if (success)
		{
			Destroy(gameObject);
			TooltipManager.instance.HideTooltip();
		}
	}

	void Awake()
	{
		interactable = GetComponent<Interactable>();
		itemName.text = item.name;
	}

	void Start()
	{
		interactable.interactEvent.AddListener(pickupItem);
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
