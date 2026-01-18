using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnvilMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public AnvilMachine Machine { get; set; }

    [SerializeField] private InventoryUi uiOutputSlots;
    [SerializeField] private FragmentListUi uiComponentCost;
    [SerializeField] private TextMeshProUGUI uiCostText;

    void UpdateComponentCost()
    {
        if (Machine.CurrentRecipe != null)
        {
            uiComponentCost.FragmentInventory = Machine.CurrentRecipe.Value.FragmentInputs;
            uiCostText.gameObject.SetActive(true);
        }
        else
        {
            uiComponentCost.FragmentInventory = new FragmentInventory();
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
