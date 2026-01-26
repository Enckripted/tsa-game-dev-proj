using UnityEngine;

//TODO: TURN THIS INTO A TILEENTITY INSTEAD OF A MACHINE
public class StorageBoxUi : MonoBehaviour
{
    [SerializeField] private InventoryUi uiStorageSlots;

    public StorageBox StorageBoxInstance { get; set; }

    void Start()
    {
        GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 70);
        uiStorageSlots.Inventory = StorageBoxInstance.StorageSlots;
    }
}
