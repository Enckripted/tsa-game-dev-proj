using UnityEngine;

public abstract class MachineUi : MonoBehaviour
{
    // jank cause unity doesnt support covariant types
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
    protected abstract IMachine MachineInstance { get; }

    [SerializeField] private InventoryUi uiInputSlots;
    [SerializeField] private StartMachineButton startMachineButton;
    [SerializeField] private RecipeDurationText recipeDurationText;

    protected abstract void OnLoad();

    void Start()
    {
        GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 70);

        uiInputSlots.Inventory = MachineInstance.InputSlots;
        startMachineButton.Machine = MachineInstance;
        recipeDurationText.Machine = MachineInstance;

        OnLoad();
    }
}
