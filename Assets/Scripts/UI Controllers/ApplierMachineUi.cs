using UnityEngine;

public class ApplierMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public ApplierMachine Machine { get; set; }

    [SerializeField] private InventoryUi outputSlotsUi;

    protected override void OnLoad()
    {
        outputSlotsUi.Inventory = Machine.OutputSlots;
    }
}
