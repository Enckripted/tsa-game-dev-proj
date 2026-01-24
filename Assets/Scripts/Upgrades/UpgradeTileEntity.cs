using UnityEngine;

public class UpgradeTileEntity : TileEntity
{
    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    public override void LoadUi(GameObject uiInstance)
    {
        throw new System.NotImplementedException();
    }

    public override void UnloadUi(GameObject uiInstance)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }
}
