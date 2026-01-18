using UnityEngine;

public class Shop : TileEntity
{
    [SerializeField] private int numSellSlots = 20;
    public Inventory SellSlots { get; private set; }

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    public override void LoadUi(GameObject uiInstance)
    {
        ShopUi ui = uiInstance.GetComponent<ShopUi>();
        ui.ShopInstance = this;

        Player.PlayerInventory.TargetInventory = SellSlots;
    }

    public override void UnloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }

    public double GetSellValue()
    {
        double sellValue = 0;
        foreach (InventorySlot slot in SellSlots)
        {
            if (!slot.ContainsItem || slot.StoredItem.Type != ItemType.WandItem) continue;
            sellValue += (slot.StoredItem as WandItem).Stats.SellValue;
        }
        return sellValue;
    }

    public void SellItems()
    {
        Player.AddMoney(GetSellValue());
        foreach (InventorySlot slot in SellSlots) slot.Pop();
    }

    protected override void OnStart()
    {
        SellSlots = new Inventory(numSellSlots);
    }
}
