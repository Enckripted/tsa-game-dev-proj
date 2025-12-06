using UnityEngine;

public class MelterMachineUi : MonoBehaviour
{
	public InventoryUi uiInputSlots;
	public ComponentListUi uiComponentList;

	private MelterMachine machine;

	void updateComponentValue()
	{
		uiComponentList.components = machine.getInputMaterialValue();
	}

	void Awake()
	{
		machine = MachineUiManager.instance.currentMachine as MelterMachine;
	}

	void Start()
	{
		uiInputSlots.inventory = machine.inputSlots;
		machine.inputSlots.changed.AddListener(updateComponentValue);
	}
}