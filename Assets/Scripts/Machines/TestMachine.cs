using System.Collections.Generic;

public class TestMachine : BaseMachine
{
	public override bool runsAutomatically
	{
		get { return true; }
	}

	public override bool hasValidRecipe()
	{
		//there are at least 2 inputs in the machine
		return inputSlots.availableSlots <= inputSlots.totalSlots - 2;
	}

	protected override Recipe getRecipe()
	{
		IEnumerable<ComponentQuantity> inputComp = new List<ComponentQuantity>();
		IEnumerable<ComponentQuantity> outputComp = new List<ComponentQuantity>();
		IEnumerable<GearItem> gears = new List<GearItem> { RandItemGen.instance.genRandomGear(), RandItemGen.instance.genRandomGear() };
		return new Recipe(3.0, inputComp, outputComp, gears);
	}

	protected override void extractItemInputs()
	{
		int removed = 0;
		for (int i = 0; i < inputSlots.totalSlots; i++)
		{
			if (!inputSlots.slotContainsItem(i)) continue;
			inputSlots.removeItemFromSlot(i);
			removed += 1;
			if (removed == 2) return;
		}
	}
}