using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockUpgradeUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI tierText;
    [SerializeField] private Button buyButton;

    public UnlockUpgrade Upgrade;

    void Start()
    {
        nameText.text = Upgrade.Name;
        buyButton.onClick.AddListener(Upgrade.BuyUpgrade);
    }

    void Update()
    {
        costText.text = Upgrade.CurrentTier < Upgrade.Tiers.Count ? $"${Upgrade.GetCurrentTier().Cost:0.00}" : "MAXED";
        tierText.text = $"Tier: {Upgrade.CurrentTier}/{Upgrade.Tiers.Count}";
    }
}
