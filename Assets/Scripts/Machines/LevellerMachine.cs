using System;
using System.Collections.Generic;
using UnityEngine;

public class LevellerMachine : BaseMachine
{
    public override int NumInputSlots => 2;
    public override int NumOutputSlots => 4;
    public override bool RunsAutomatically => false;
    public override bool StopsWhenFinished => true;

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }
    [field: SerializeField] private AudioClip finishSfx;

    private bool AllInputsIdentical()
    {
        if (InputSlots.AvailableSlots != 0) return false;

        WandItem reference = InputSlots.ItemInSlot(0) as WandItem;
        for (int i = 0; i < NumInputSlots; i++)
        {
            WandItem cur = InputSlots.ItemInSlot(i) as WandItem;
            //TODO: probably replace this with some sort of custom operator on WandItem?
            if (cur.BaseName != reference.BaseName ||
            cur.Level != reference.Level ||
            cur.WandMaterial.Name != reference.WandMaterial.Name ||
            cur.WandReforge != null && reference.WandReforge != null && cur.WandReforge.Name != reference.WandReforge.Name) return false;
        }
        return true;
    }

    public override bool HasValidRecipe()
    {
        return AllInputsIdentical();
    }

    protected override Recipe GetRecipe()
    {
        WandItem reference = InputSlots.ItemInSlot(0) as WandItem;
        WandItem other = InputSlots.ItemInSlot(1) as WandItem;
        WandItem output = new WandItem(reference.BaseName, reference.Level + 1, reference.BaseStats, reference.LevelStats, reference.WandMaterial,
        Math.Min(reference.GemSlots, other.GemSlots), null, reference.WandReforge);

        IEnumerable<IItem> itemOutputs = new List<WandItem> { output };
        return new Recipe(6.0, new FragmentInventory(), new FragmentInventory(), itemOutputs);
    }

    protected override void ExtractItemInputs()
    {
        for (int i = 0; i < NumInputSlots; i++)
            InputSlots.GetSlot(i).Pop();
    }

    protected override void OnRecipeEnd()
    {
        MachineAudioSource.PlayOneShot(finishSfx);
    }

    protected override void MachineUpdate() { }

    protected override void LoadMachineIntoUi(GameObject uiInstance)
    {
        LevellerMachineUi ui = uiInstance.GetComponent<LevellerMachineUi>();
        ui.Machine = this;
    }
}
