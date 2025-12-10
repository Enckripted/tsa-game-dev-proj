using System.Collections.Generic;
using UnityEngine;

public class LevellerMachine : BaseMachine
{
	public override int numInputSlots => 4;
	public override int numOutputSlots => 9;
	public override bool runsAutomatically => false;
	public override bool stopsWhenFinished => true;

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }
	[field: SerializeField] private AudioClip finishSfx;

	private bool allInputsIdentical()
	{
		if (inputSlots.availableSlots != 0) return false;

		GearItem reference = inputSlots.itemInSlot(0) as GearItem;
		for (int i = 0; i < numInputSlots; i++)
		{
			GearItem cur = inputSlots.itemInSlot(i) as GearItem;
			//TODO: probably replace this with some sort of custom operator on Gear?
			if (cur.data.baseName != reference.data.baseName ||
			cur.data.level != reference.data.level ||
			cur.data.material.name != reference.data.material.name) return false;
		}
		return true;
	}

	public override bool hasValidRecipe()
	{
		return allInputsIdentical();
	}

	protected override Recipe getRecipe()
	{
		Gear reference = (inputSlots.itemInSlot(0) as GearItem).data;
		Gear output = new Gear(reference.baseName, reference.level + 1, reference.baseStats, reference.levelStats, reference.material);

		IEnumerable<ComponentQuantity> componentInputs = new List<ComponentQuantity> { };
		IEnumerable<ComponentQuantity> componentOutputs = new List<ComponentQuantity> { };
		IEnumerable<Item> itemOutputs = new List<GearItem> { new GearItem(output) };
		return new Recipe(6.0, componentInputs, componentOutputs, itemOutputs);
	}

	protected override void extractItemInputs()
	{
		for (int i = 0; i < numInputSlots; i++)
			inputSlots.getSlot(i).pop();
	}

	protected override void onRecipeEnd()
	{
		audioSource.PlayOneShot(finishSfx);
	}

	protected override void machineUpdate() { }

	protected override void loadMachineIntoUi(GameObject uiInstance)
	{
		LevellerMachineUi ui = uiInstance.GetComponent<LevellerMachineUi>();
		ui.machine = this;
	}
}