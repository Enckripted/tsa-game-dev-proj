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

    public IEnumerable<ComponentQuantity> GetInputMaterialValue()
    {
        Dictionary<string, uint> quant = new Dictionary<string, uint>();
        for (int i = 0; i < InputSlots.TotalSlots; i++)
        {
            if (InputSlots.SlotContainsItem(i) && InputSlots.ItemInSlot(i).Type == ItemType.WandItem)
            {

                WandItem currentGear = InputSlots.ItemInSlot(i) as WandItem;
                if (!quant.ContainsKey(currentGear.WandMaterial.Name)) quant.Add(currentGear.WandMaterial.Name, 0);
                quant[currentGear.WandMaterial.Name] += 5;
            }
        }

        List<ComponentQuantity> components = new List<ComponentQuantity>();
        foreach (KeyValuePair<string, uint> pair in quant)
        {
            components.Add(new ComponentQuantity(pair.Key, pair.Value));
        }
        return components;
    }

    protected override Recipe GetRecipe()
    {
        WandItem inputItem = InputSlots.ItemInSlot(FindSlotWithWandItem()) as WandItem;

        IEnumerable<ComponentQuantity> inputComp = new List<ComponentQuantity> { };
        IEnumerable<ComponentQuantity> outputComp = new List<ComponentQuantity> { new ComponentQuantity(inputItem.WandMaterial.Name, 5) };
        IEnumerable<IItem> itemOutput = new List<IItem> { };
        return new Recipe(2.0, inputComp, outputComp, itemOutput);
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
