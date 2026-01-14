using System.Collections.Generic;
using UnityEngine;

public class MelterMachine : BaseMachine
{
    public override int numInputSlots => 12;
    public override int numOutputSlots => 0;
    public override bool runsAutomatically => false;
    public override bool stopsWhenFinished => false;

    [field: SerializeField] public override GameObject uiPrefab { get; protected set; }
    [SerializeField] private AudioClip soundEffect;

    //returns -1 if nothing is found
    private int findSlotWithWandItem()
    {
        for (int i = 0; i < inputSlots.TotalSlots; i++)
            if (inputSlots.SlotContainsItem(i) && inputSlots.ItemInSlot(i).Type == ItemType.WandItem)
                return i;
        return -1;
    }

    public override bool hasValidRecipe()
    {
        return findSlotWithWandItem() != -1;
    }

    public IEnumerable<ComponentQuantity> getInputMaterialValue()
    {
        Dictionary<string, uint> quant = new Dictionary<string, uint>();
        for (int i = 0; i < inputSlots.TotalSlots; i++)
        {
            if (inputSlots.SlotContainsItem(i) && inputSlots.ItemInSlot(i).Type == ItemType.WandItem)
            {

                WandItem currentGear = inputSlots.ItemInSlot(i) as WandItem;
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

    protected override Recipe getRecipe()
    {
        WandItem inputItem = inputSlots.ItemInSlot(findSlotWithWandItem()) as WandItem;

        IEnumerable<ComponentQuantity> inputComp = new List<ComponentQuantity> { };
        IEnumerable<ComponentQuantity> outputComp = new List<ComponentQuantity> { new ComponentQuantity(inputItem.WandMaterial.Name, 5) };
        IEnumerable<IItem> itemOutput = new List<IItem> { };
        return new Recipe(2.0, inputComp, outputComp, itemOutput);
    }

    protected override void extractItemInputs()
    {
        inputSlots.RemoveItemFromSlot(findSlotWithWandItem());
    }

    protected override void onRecipeEnd()
    {
        SoundManager.instance.playSound(soundEffect);
    }

    protected override void machineUpdate() { }

    protected override void loadMachineIntoUi(GameObject uiInstance)
    {
        MelterMachineUi ui = uiInstance.GetComponent<MelterMachineUi>();
        ui.machine = this;
    }
}
