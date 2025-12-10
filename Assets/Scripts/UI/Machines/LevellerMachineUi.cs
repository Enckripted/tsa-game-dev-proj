using UnityEngine;

public class LevellerMachineUi : MachineUi
{
	protected override IMachine _machine => machine;
	public LevellerMachine machine { get; set; }

	[SerializeField] private InventoryUi uiOutputSlots;

	protected override void onLoad()
	{
		uiOutputSlots.inventory = machine.outputSlots;
	}
}