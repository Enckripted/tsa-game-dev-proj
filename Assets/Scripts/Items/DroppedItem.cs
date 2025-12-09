using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public TextMeshPro itemName;

	public Item item;

	private Interactable interactable;
	private SpriteRenderer spriteRenderer;

	void pickupItem()
	{
		bool success = PlayerInventory.instance.inventory.pushItem(item);
		if (success)
		{
			Destroy(gameObject);
			TooltipManager.instance.HideTooltip();
		}
	}

	void loadSprite()
	{
		Sprite sprite;
		if (item.type == ItemType.Gear)
		{
			GearItem gearItem = item as GearItem;
			sprite = ItemSpriteManager.instance.getItemSpriteFor(gearItem.data.baseName, gearItem.data.material);
		}
		else
		{
			throw new System.Exception("this code path shouldn't run unless we add new items!");
		}
		Debug.Log(spriteRenderer);
		Debug.Log(sprite);
		spriteRenderer.sprite = sprite;
	}

	void Awake()
	{
		interactable = GetComponent<Interactable>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		loadSprite();
		//itemName.text = item.name;
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
