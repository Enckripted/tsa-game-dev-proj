//this is a very jank way to add a storage container but we'll roll with it for now
using UnityEngine;

public class StorageBox : TileEntity
{
    [SerializeField] private int numStorageSlots;
    public Inventory StorageSlots { get; private set; }

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    public override void LoadUi(GameObject uiInstance)
    {
        StorageBoxUi ui = uiInstance.GetComponent<StorageBoxUi>();
        ui.StorageBoxInstance = this;

        Player.PlayerInventory.TargetInventory = StorageSlots;
        StorageSlots.TargetInventory = Player.PlayerInventory;
    }

    public override void UnloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }

    protected override void OnStart()
    {
        StorageSlots = new Inventory(numStorageSlots);
    }
}
