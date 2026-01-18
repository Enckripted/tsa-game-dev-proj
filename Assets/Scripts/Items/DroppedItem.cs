using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : Entity, IPointerEnterHandler, IPointerExitHandler
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
            TooltipManager.Instance.HideTooltip();
        }
    }

    void LoadSprite()
    {
        Sprite sprite;
        if (Item.Type == ItemType.WandItem)
        {
            WandItem wandItem = Item as WandItem;
            sprite = ItemSpriteService.GetItemSpriteFor(wandItem.BaseName, wandItem.WandMaterial);
        }
        else
        {
            throw new System.Exception("this code path shouldn't run unless we add new items!");
        }
        spriteRenderer.sprite = sprite;
    }

    protected override void OnInteract() => PickupItem();
    protected override void OnStart()
    {
        interactable = GetComponent<Interactable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSprite();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.ShowTooltip(Item.ItemTooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
