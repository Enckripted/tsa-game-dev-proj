using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshPro ItemName;

    public IItem Item;

    private Interactable interactable;
    private SpriteRenderer spriteRenderer;

    void PickupItem()
    {
        bool success = Player.PlayerInventory.PushItem(Item);
        if (success)
        {
            Destroy(gameObject);
            TooltipManager.instance.HideTooltip();
        }
    }

    void LoadSprite()
    {
        Sprite sprite;
        if (Item.Type == ItemType.WandItem)
        {
            WandItem wandItem = Item as WandItem;
            sprite = ItemSpriteManager.instance.getItemSpriteFor(wandItem.BaseName, wandItem.WandMaterial);
        }
        else
        {
            throw new System.Exception("this code path shouldn't run unless we add new items!");
        }
        spriteRenderer.sprite = sprite;
    }

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSprite();
        //itemName.text = item.name;
    }

    void Start()
    {
        interactable.interactEvent.AddListener(PickupItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.ShowTooltip(Item.ItemTooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideTooltip();
    }
}
