using UnityEngine;

public abstract class MachineUi : MonoBehaviour
{
    // jank cause unity doesnt support covariant types
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
    protected abstract IMachine _machine { get; }

    [SerializeField] private InventoryUi uiInputSlots;
    [SerializeField] private StartMachineButton startMachineButton;
    [SerializeField] private RecipeDurationText recipeDurationText;

    protected abstract void onLoad();

    void Start()
    {
        uiInputSlots.inventory = _machine.inputSlots;
        startMachineButton.machine = _machine;
        recipeDurationText.machine = _machine;

        onLoad();
    }
}
