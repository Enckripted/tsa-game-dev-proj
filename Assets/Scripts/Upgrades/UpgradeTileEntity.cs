using System.Collections.Generic;
using UnityEngine;

public class UpgradeTileEntity : TileEntity
{
    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    [SerializeField] private bool giveAllUpgradesInDevMode;
    [field: SerializeField] public List<UnlockUpgrade> UnlockUpgrades { get; private set; }


    public override void LoadUi(GameObject uiInstance)
    {
        UpgradeUi ui = uiInstance.GetComponent<UpgradeUi>();
        ui.TileEntity = this;
    }

    public override void UnloadUi(GameObject uiInstance) { }

    protected override void OnStart()
    {
        if (!Debug.isDebugBuild || !giveAllUpgradesInDevMode) return;

        foreach (UnlockUpgrade unlockUpgrade in UnlockUpgrades)
        {
            foreach (UnlockUpgradeTier upgradeTier in unlockUpgrade.Tiers)
            {
                Player.AddMoney(upgradeTier.Cost);
                unlockUpgrade.BuyUpgrade();
            }
        }
    }
}
