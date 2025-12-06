using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnvilMachineUi : MonoBehaviour
{
	public InventoryUi uiInputSlots;
	public InventoryUi uiOutputSlots;
	public ComponentListUi uiComponentCost;
	public TextMeshProUGUI uiCostText;

	private AnvilMachine machine;

	void updateComponentCost()
	{
		if (machine.currentRecipe != null)
		{
			uiComponentCost.components = machine.currentRecipe.Value.componentInputs;
			uiCostText.gameObject.SetActive(true);
		}
		else
		{
			uiComponentCost.components = new List<ComponentQuantity>();
			uiCostText.gameObject.SetActive(true);
		}
	}

	void Awake()
	{
		machine = MachineUiManager.instance.currentMachine as AnvilMachine;
	}

	void Start()
	{
		uiInputSlots.inventory = machine.inputSlots;
		uiOutputSlots.inventory = machine.outputSlots;

		machine.inputSlots.changed.AddListener(updateComponentCost);
		updateComponentCost();
	}

}