using UnityEngine;

public class MelterMachineUi : MachineUi
{
    protected override IMachine _machine => machine;
    public MelterMachine machine { get; set; }

    [SerializeField] private ComponentListUi componentListUi;

    void updateComponentValue()
    {
        componentListUi.components = machine.getInputMaterialValue();
    }

    protected override void onLoad()
    {
        machine.inputSlots.Changed.AddListener(updateComponentValue);
        updateComponentValue();
    }
}
