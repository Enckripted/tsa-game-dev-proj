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
		machine = NewMachineUiManager.instance.currentMachine as MelterMachine;
		uiInputSlots.inventory = machine.inputSlots;
		machine.inputSlots.changed.AddListener(updateComponentValue);
	}
}