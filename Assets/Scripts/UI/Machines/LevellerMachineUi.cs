using UnityEngine;

public class LevellerMachineUi : MonoBehaviour
{
	public InventoryUi uiInputSlots;
	public InventoryUi uiOutputSlots;

	private LevellerMachine machine;

	void Awake()
	{
		machine = MachineUiManager.instance.currentMachine as LevellerMachine;
		uiInputSlots.inventory = machine.inputSlots;
		uiOutputSlots.inventory = machine.outputSlots;
	}
}