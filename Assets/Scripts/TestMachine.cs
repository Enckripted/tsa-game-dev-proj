using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMachine : BaseMachine
{

	public override bool canRunRecipe()
	{
		//there are at least 2 inputs in the machine
		return inputSlots.availableSlots <= inputSlots.totalSlots - 2;
	}

	protected override Recipe getRecipe()
	{
		IEnumerable<GearItem> gears = new List<GearItem> { RandItemGen.instance.genRandomGear(), RandItemGen.instance.genRandomGear() };
		return new Recipe(3.0, gears);
	}

	protected override void extractRecipeInputs()
	{
		int removed = 0;
		for (int i = 0; i < inputSlots.totalSlots; i++)
		{
			if (!inputSlots.itemInSlot(i)) continue;
			inputSlots.removeItemFromSlot(i);
			removed += 1;
			if (removed == 2) return;
		}
	}
}