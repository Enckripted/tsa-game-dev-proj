using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
	public InventoryUi uiSellSlots;

	public Shop shop;
	public Button sellButton;

	void sellItems()
	{
		shop.sellItems();
	}

	void Start()
	{
		uiSellSlots.inventory = shop.sellSlots;
		sellButton.onClick.AddListener(sellItems);
	}
}