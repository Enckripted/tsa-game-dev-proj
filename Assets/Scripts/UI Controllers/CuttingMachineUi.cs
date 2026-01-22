using UnityEngine;

public class CuttingMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public CuttingMachine Machine { get; set; }

    [SerializeField] private InventoryUi outputSlotsUi;

    protected override void OnLoad()
    {
        outputSlotsUi.Inventory = Machine.OutputSlots;
    }
}
