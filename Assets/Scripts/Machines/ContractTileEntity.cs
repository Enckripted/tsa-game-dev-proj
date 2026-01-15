using UnityEngine;

public class ContractTileEntity : TileEntity
{
	[field: SerializeField] public override GameObject UiPrefab { get; protected set; }

	protected override void OnStart()
	{
	}

	public override void LoadUi(GameObject uiInstance)
	{
	}

	public override void UnloadUi(GameObject uiInstance)
	{
	}
}
