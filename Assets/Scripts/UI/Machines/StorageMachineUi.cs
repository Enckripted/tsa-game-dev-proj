using UnityEngine;

public class StorageMachineUi : MonoBehaviour
{
	public InventoryUi uiInputSlots;

	private StorageMachine machine;

	void Awake()
	{
		machine = MachineUiManager.instance.currentMachine as StorageMachine;
	}

	void Start()
	{
		uiInputSlots.inventory = machine.inputSlots;
	}
}