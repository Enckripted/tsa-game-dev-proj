using UnityEngine;

public class UpgradeUi : MonoBehaviour
{
    [SerializeField] private GameObject unlockUpgradeElement;
    [SerializeField] private Transform upgradeContainer;

    public UpgradeTileEntity TileEntity { get; set; }

    void Start()
    {
        foreach (UnlockUpgrade upgrade in TileEntity.UnlockUpgrades)
        {
            UnlockUpgradeUi upgradeTierElement = Instantiate(unlockUpgradeElement, upgradeContainer).GetComponent<UnlockUpgradeUi>();
            upgradeTierElement.Upgrade = upgrade;
        }
    }
}
