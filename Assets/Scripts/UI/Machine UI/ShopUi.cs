using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    [SerializeField] public InventoryUi UiSellSlots;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button sellButton;

    public Shop ShopInstance { get; set; }

    void SellItems()
    {
        ShopInstance.SellItems();
    }

    void UpdateValueText()
    {
        valueText.text = $"${ShopInstance.GetSellValue():0.00}";
    }

    void Start()
    {
        UiSellSlots.Inventory = ShopInstance.SellSlots;
        sellButton.onClick.AddListener(SellItems);
        ShopInstance.SellSlots.Changed.AddListener(UpdateValueText);
        UpdateValueText();
    }
}
