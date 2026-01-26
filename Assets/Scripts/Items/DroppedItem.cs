using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactable))]
public class DroppedItem : Entity//, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshPro ItemName;

    public IItem Item;

    private SpriteRenderer spriteRenderer;

    void PickupItem()
    {
        bool success = Player.PlayerInventory.PushItem(Item);
        if (success)
        {
            Player.ItemsPickedUp++;
            Destroy(gameObject);
            //TooltipManager.Instance.HideTooltip();
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
        else if (Item.Type == ItemType.GemItem)
        {
            GemItem gemItem = Item as GemItem;
            sprite = gemItem.Sprite;
        }
        else
        {
            throw new System.Exception("this code path shouldn't run unless we add new items!");
        }
        if (sprite == null) ItemName.text = Item.Name;
        else spriteRenderer.sprite = sprite;
    }

    protected override void OnInteract() => PickupItem();
    protected override void OnStart()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSprite();
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.ShowTooltip(Item.HoverTooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }*/

}
