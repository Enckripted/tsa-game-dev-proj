using System.Collections.Generic;
using UnityEngine;

public class MelterMachine : BaseMachine
{
    public override int NumInputSlots => 12;
    public override int NumOutputSlots => 0;
    public override bool RunsAutomatically => false;
    public override bool StopsWhenFinished => false;

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }
    [SerializeField] private AudioClip soundEffect;

    //returns -1 if nothing is found
    private int FindSlotWithWandItem()
    {
        for (int i = 0; i < InputSlots.TotalSlots; i++)
            if (InputSlots.SlotContainsItem(i) && InputSlots.ItemInSlot(i).Type == ItemType.WandItem)
                return i;
        return -1;
    }

    public override bool HasValidRecipe()
    {
        return FindSlotWithWandItem() != -1;
    }

    public FragmentInventory GetInputMaterialValue()
    {
        FragmentInventory fragments = new FragmentInventory();
        for (int i = 0; i < InputSlots.TotalSlots; i++)
        {
            if (InputSlots.SlotContainsItem(i) && InputSlots.ItemInSlot(i).Type == ItemType.WandItem)
            {
                WandItem currentGear = InputSlots.ItemInSlot(i) as WandItem;
                fragments.AddFragmentQuantity(new FragmentQuantity(currentGear.WandMaterial, 5));
            }
        }
        return fragments;
    }

    protected override Recipe GetRecipe()
    {
        WandItem inputItem = InputSlots.ItemInSlot(FindSlotWithWandItem()) as WandItem;

        FragmentInventory outputComp = new FragmentInventory();
        outputComp.AddFragmentQuantity(new FragmentQuantity(inputItem.WandMaterial, 5));

        return new Recipe(2.0, new FragmentInventory(), outputComp, new List<IItem>());
    }

    protected override void ExtractItemInputs()
    {
        InputSlots.RemoveItemFromSlot(FindSlotWithWandItem());
    }

    protected override void OnRecipeEnd()
    {
        //TODO: replace with proper sound playing
        //SoundManager.instance.playSound(soundEffect);
    }

    protected override void MachineUpdate() { }

    protected override void LoadMachineIntoUi(GameObject uiInstance)
    {
        MelterMachineUi ui = uiInstance.GetComponent<MelterMachineUi>();
        ui.Machine = this;
    }
}
