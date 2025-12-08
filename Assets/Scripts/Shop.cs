using UnityEngine;

public class Shop : TileEntity
{
	[SerializeField] private const int numSellSlots = 24;
	public Inventory sellSlots { get; private set; }

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	public override void loadUi(GameObject uiInstance)
	{
		ShopUi ui = uiInstance.GetComponent<ShopUi>();
		ui.shop = this;

		PlayerInventory.instance.inventory.targetInventory = sellSlots;
	}

	public override void unloadUi(GameObject uiInstance)
	{
		PlayerInventory.instance.inventory.targetInventory = null;
	}

	public double getSellValue()
	{
		double sellValue = 0;
		foreach (InventorySlot slot in sellSlots)
		{
			if (!slot.containsItem || slot.item.type != ItemType.Gear) continue;
			sellValue += (slot.item as GearItem).data.gearStats.sellValue;
		}
		return sellValue;
	}

	public void sellItems()
	{
		PlayerInventory.instance.addMoney(getSellValue());
		foreach (InventorySlot slot in sellSlots) slot.pop();
	}

	protected override void onStart()
	{
		sellSlots = new Inventory(numSellSlots);
	}
}