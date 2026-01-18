using UnityEngine;

public class LevellerMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public LevellerMachine Machine { get; set; }

    [SerializeField] private InventoryUi uiOutputSlots;

    protected override void OnLoad()
    {
        uiOutputSlots.Inventory = Machine.OutputSlots;
    }
}
