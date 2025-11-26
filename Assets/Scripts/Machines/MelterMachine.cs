using System.Collections.Generic;
using UnityEngine;

public class MelterMachine : BaseMachine
{
	public override bool runsAutomatically
	{
		get { return false; }
	}

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
}