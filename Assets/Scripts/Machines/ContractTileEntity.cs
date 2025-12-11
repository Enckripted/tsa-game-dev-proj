using UnityEngine;

public class ContractTileEntity : TileEntity
{
	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }

	protected override void onStart()
	{
	}

	public override void loadUi(GameObject uiInstance)
	{
	}

	public override void unloadUi(GameObject uiInstance)
	{
	}
}