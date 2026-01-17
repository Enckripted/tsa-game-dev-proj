using UnityEngine;

//TODO: TURN THIS INTO A TILEENTITY INSTEAD OF A MACHINE
public class StorageBoxUi : MonoBehaviour
{
    [SerializeField] private InventoryUi uiStorageSlots;

    public StorageBox StorageBoxInstance { get; set; }

    void Start()
    {
        uiStorageSlots.Inventory = StorageBoxInstance.StorageSlots;
    }
}
