using UnityEngine;

public class LevellerMachineUi : MonoBehaviour
{
	public InventoryUi uiInputSlots;
	public InventoryUi uiOutputSlots;

	private LevellerMachine machine;

	void Awake()
	{
		machine = MachineUiManager.instance.currentMachine as LevellerMachine;

	}

	void Start()
	{
		uiInputSlots.inventory = machine.inputSlots;
		uiOutputSlots.inventory = machine.outputSlots;
	}
}