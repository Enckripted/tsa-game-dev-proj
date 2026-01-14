using System.Collections.Generic;
using UnityEngine;

public class LevellerMachine : BaseMachine
{
    public override int numInputSlots => 4;
    public override int numOutputSlots => 9;
    public override bool runsAutomatically => false;
    public override bool stopsWhenFinished => true;

    [field: SerializeField] public override GameObject uiPrefab { get; protected set; }
    [field: SerializeField] private AudioClip finishSfx;

    private bool allInputsIdentical()
    {
        if (inputSlots.AvailableSlots != 0) return false;

        WandItem reference = inputSlots.ItemInSlot(0) as WandItem;
        for (int i = 0; i < numInputSlots; i++)
        {
            WandItem cur = inputSlots.ItemInSlot(i) as WandItem;
            //TODO: probably replace this with some sort of custom operator on WandItem?
            if (cur.BaseName != reference.BaseName ||
            cur.Level != reference.Level ||
            cur.WandMaterial.Name != reference.WandMaterial.Name) return false;
        }
        return true;
    }

    public override bool hasValidRecipe()
    {
        return allInputsIdentical();
    }

    protected override Recipe getRecipe()
    {
        WandItem reference = (inputSlots.ItemInSlot(0) as WandItem);
        WandItem output = new WandItem(reference.BaseName, reference.Level + 1, reference.BaseStats, reference.LevelStats, reference.WandMaterial);

        IEnumerable<ComponentQuantity> componentInputs = new List<ComponentQuantity> { };
        IEnumerable<ComponentQuantity> componentOutputs = new List<ComponentQuantity> { };
        IEnumerable<IItem> itemOutputs = new List<WandItem> { output };
        return new Recipe(6.0, componentInputs, componentOutputs, itemOutputs);
    }

    protected override void extractItemInputs()
    {
        for (int i = 0; i < numInputSlots; i++)
            inputSlots.GetSlot(i).Pop();
    }

    protected override void onRecipeEnd()
    {
        audioSource.PlayOneShot(finishSfx);
    }

    protected override void machineUpdate() { }

    protected override void loadMachineIntoUi(GameObject uiInstance)
    {
        LevellerMachineUi ui = uiInstance.GetComponent<LevellerMachineUi>();
        ui.machine = this;
    }
}
