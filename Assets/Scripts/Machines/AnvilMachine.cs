using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilMachine : BaseMachine
{
	public override int numInputSlots => 1;
	public override int numOutputSlots => 1;
	public override bool runsAutomatically => false;
	public override bool stopsWhenFinished => true;

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	public override bool hasValidRecipe()
	{
		return inputSlots.availableSlots == 0;
	}

	protected override Recipe getRecipe()
	{
		Gear reference = (inputSlots.itemInSlot(0) as GearItem).data;
		Reforge reforge = ScriptableObjectData.instance.getRandomReforge();
		Gear output = new Gear(reference.baseName, reference.level, reference.baseStats, reference.levelStats, reference.material, reforge);

		IEnumerable<ComponentQuantity> componentInputs = new List<ComponentQuantity> { new ComponentQuantity(reference.material.name, 10) };
		IEnumerable<ComponentQuantity> componentOutputs = new List<ComponentQuantity> { };
		IEnumerable<Item> itemOutputs = new List<GearItem> { new GearItem(output) };
		return new Recipe(10.0, componentInputs, componentOutputs, itemOutputs);
	}

	protected override void extractItemInputs()
	{
		inputSlots.getSlot(0).pop();
	}

	protected override void onRecipeEnd() { }

	protected override void loadMachineIntoUi(GameObject uiInstance)
	{
		AnvilMachineUi ui = uiInstance.GetComponent<AnvilMachineUi>();
		ui.machine = this;
	}
}