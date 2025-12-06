//this is a very jank way to add a storage container but we'll roll with it for now
using UnityEngine;

public class StorageMachine : BaseMachine
{
	public override int numInputSlots => 18;
	public override int numOutputSlots => 1;
	public override bool runsAutomatically => false;
	public override bool stopsWhenFinished => false;

	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	public override bool hasValidRecipe() => false;

	protected override Recipe getRecipe() => new Recipe();

	protected override void extractItemInputs() { }
}