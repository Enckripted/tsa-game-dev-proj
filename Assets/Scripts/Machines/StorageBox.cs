//this is a very jank way to add a storage container but we'll roll with it for now
using UnityEngine;

public class StorageBox : TileEntity
{
	[SerializeField] private int numStorageSlots;
	public Inventory storageSlots { get; private set; }

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	public override void loadUi(GameObject uiInstance)
	{
		StorageBoxUi ui = uiInstance.GetComponent<StorageBoxUi>();
		ui.storageBox = this;

		PlayerInventory.instance.inventory.targetInventory = storageSlots;
	}

	public override void unloadUi(GameObject uiInstance)
	{
		PlayerInventory.instance.inventory.targetInventory = null;
	}

	protected override void onStart()
	{
		storageSlots = new Inventory(numStorageSlots);
	}
}