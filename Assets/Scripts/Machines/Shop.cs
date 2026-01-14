using UnityEngine;

public class Shop : TileEntity
{
    [SerializeField] private int numSellSlots = 20;
    public Inventory sellSlots { get; private set; }

    [field: SerializeField] public override GameObject uiPrefab { get; protected set; }

    public override void loadUi(GameObject uiInstance)
    {
        ShopUi ui = uiInstance.GetComponent<ShopUi>();
        ui.shop = this;

        Player.PlayerInventory.TargetInventory = sellSlots;
    }

    public override void unloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }

    public double getSellValue()
    {
        double sellValue = 0;
        foreach (InventorySlot slot in sellSlots)
        {
            if (!slot.ContainsItem || slot.StoredItem.Type != ItemType.WandItem) continue;
            sellValue += (slot.StoredItem as WandItem).Stats.SellValue;
        }
        return sellValue;
    }

    public void sellItems()
    {
        Player.AddMoney(getSellValue());
        foreach (InventorySlot slot in sellSlots) slot.Pop();
    }

    protected override void onStart()
    {
        sellSlots = new Inventory(numSellSlots);
    }
}
