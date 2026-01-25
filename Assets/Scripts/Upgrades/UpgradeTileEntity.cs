using System.Collections.Generic;
using UnityEngine;

public class UpgradeTileEntity : TileEntity
{
    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    [field: SerializeField] public List<UnlockUpgrade> UnlockUpgrades { get; private set; }

    public override void LoadUi(GameObject uiInstance)
    {
        UpgradeUi ui = uiInstance.GetComponent<UpgradeUi>();
        ui.TileEntity = this;
    }

    public override void UnloadUi(GameObject uiInstance) { }

    protected override void OnStart() { }
}
