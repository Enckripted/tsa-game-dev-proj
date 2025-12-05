using System.Collections.Generic;
using UnityEngine;

public class TestMachine : BaseMachine
{
	public override int numInputSlots => 12;
	public override int numOutputSlots => 12;
	public override bool runsAutomatically => true;
	public override bool stopsWhenFinished => true;

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	public override bool hasValidRecipe()
	{
		//there are at least 2 inputs in the machine
		return inputSlots.availableSlots <= inputSlots.totalSlots - 2;
	}

	protected override Recipe getRecipe()
	{
		IEnumerable<ComponentQuantity> inputComp = new List<ComponentQuantity>();
		IEnumerable<ComponentQuantity> outputComp = new List<ComponentQuantity>();
		IEnumerable<GearItem> gears = new List<GearItem> { RandomItemFactory.instance.createRandomItem(), RandomItemFactory.instance.createRandomItem() };
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