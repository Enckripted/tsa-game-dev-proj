using UnityEngine;

public class MelterMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public MelterMachine Machine { get; set; }

    [SerializeField] private FragmentListUi componentListUi;

    void UpdateComponentValue()
    {
        componentListUi.FragmentInventory = Machine.GetInputMaterialValue();
    }

    protected override void OnLoad()
    {
        Machine.InputSlots.Changed.AddListener(UpdateComponentValue);
        UpdateComponentValue();
    }
}
