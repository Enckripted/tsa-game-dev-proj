using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		TooltipManager.instance.ShowTooltip(item);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		TooltipManager.instance.HideTooltip();
	}
}
