using UnityEngine;

//TODO: TURN THIS INTO A TILEENTITY INSTEAD OF A MACHINE
public class StorageBoxUi : MonoBehaviour
{
    [SerializeField] private InventoryUi uiStorageSlots;

    public StorageBox storageBox { get; set; }

    void Start()
    {
        uiStorageSlots.inventory = storageBox.storageSlots;
    }
}
