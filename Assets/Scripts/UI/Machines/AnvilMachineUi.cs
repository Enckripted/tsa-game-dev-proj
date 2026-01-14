using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnvilMachineUi : MachineUi
{
    protected override IMachine _machine => machine;
    public AnvilMachine machine { get; set; }

    [SerializeField] private InventoryUi uiOutputSlots;
    [SerializeField] private ComponentListUi uiComponentCost;
    [SerializeField] private TextMeshProUGUI uiCostText;

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

    protected override void onLoad()
    {
        uiOutputSlots.inventory = machine.outputSlots;
        machine.inputSlots.Changed.AddListener(updateComponentCost);
        updateComponentCost();
    }
}
