using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnvilMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public AnvilMachine Machine { get; set; }

    [SerializeField] private InventoryUi uiOutputSlots;
    [SerializeField] private ComponentListUi uiComponentCost;
    [SerializeField] private TextMeshProUGUI uiCostText;

    void UpdateComponentCost()
    {
        if (Machine.CurrentRecipe != null)
        {
            uiComponentCost.Components = Machine.CurrentRecipe.Value.ComponentInputs;
            uiCostText.gameObject.SetActive(true);
        }
        else
        {
            uiComponentCost.Components = new List<FragmentQuantity>();
            uiCostText.gameObject.SetActive(true);
        }
    }

    protected override void OnLoad()
    {
        uiOutputSlots.Inventory = Machine.OutputSlots;
        Machine.InputSlots.Changed.AddListener(UpdateComponentCost);
        UpdateComponentCost();
    }
}
