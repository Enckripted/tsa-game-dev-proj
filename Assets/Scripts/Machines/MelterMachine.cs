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
	private int findSlotWithGearItem()
	{
		for (int i = 0; i < inputSlots.totalSlots; i++)
			if (inputSlots.slotContainsItem(i) && inputSlots.itemInSlot(i).type == ItemType.Gear)
				return i;
		return -1;
	}

	public override bool hasValidRecipe()
	{
		return findSlotWithGearItem() != -1;
	}

	public IEnumerable<ComponentQuantity> getInputMaterialValue()
	{
		Dictionary<string, uint> quant = new Dictionary<string, uint>();
		for (int i = 0; i < inputSlots.totalSlots; i++)
		{
			if (inputSlots.slotContainsItem(i) && inputSlots.itemInSlot(i).type == ItemType.Gear)
			{

				GearItem currentGear = inputSlots.itemInSlot(i) as GearItem;
				if (!quant.ContainsKey(currentGear.data.material.name)) quant.Add(currentGear.data.material.name, 0);
				quant[currentGear.data.material.name] += 5;
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
		GearItem inputItem = inputSlots.itemInSlot(findSlotWithGearItem()) as GearItem;

		IEnumerable<ComponentQuantity> inputComp = new List<ComponentQuantity> { };
		IEnumerable<ComponentQuantity> outputComp = new List<ComponentQuantity> { new ComponentQuantity(inputItem.data.material.name, 5) };
		IEnumerable<Item> itemOutput = new List<Item> { };
		return new Recipe(2.0, inputComp, outputComp, itemOutput);
	}

	protected override void extractItemInputs()
	{
		inputSlots.removeItemFromSlot(findSlotWithGearItem());
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