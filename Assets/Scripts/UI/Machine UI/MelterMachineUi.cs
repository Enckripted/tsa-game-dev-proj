using UnityEngine;

public class MelterMachineUi : MachineUi
{
    protected override IMachine MachineInstance => Machine;
    public MelterMachine Machine { get; set; }

    [SerializeField] private ComponentListUi componentListUi;

    void UpdateComponentValue()
    {
        componentListUi.Components = Machine.GetInputMaterialValue();
    }

    protected override void OnLoad()
    {
        Machine.InputSlots.Changed.AddListener(UpdateComponentValue);
        UpdateComponentValue();
    }
}
