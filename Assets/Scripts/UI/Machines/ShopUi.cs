using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
	[SerializeField] public InventoryUi uiSellSlots;
	[SerializeField] private TextMeshProUGUI valueText;
	[SerializeField] private Button sellButton;

	public Shop shop { get; set; }

	void sellItems()
	{
		shop.sellItems();
	}

	void updateValueText()
	{
		valueText.text = $"${shop.getSellValue():0.00}";
	}

	void Start()
	{
		uiSellSlots.inventory = shop.sellSlots;
		sellButton.onClick.AddListener(sellItems);
		shop.sellSlots.changed.AddListener(updateValueText);
		updateValueText();
	}
}