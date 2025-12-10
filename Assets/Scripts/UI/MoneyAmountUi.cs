//TEMPORARY SCRIPT: REFACTOR LATER
using TMPro;
using UnityEngine;

public class MoneyAmountUi : MonoBehaviour
{
	private TextMeshProUGUI label;

	void Awake()
	{
		label = GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		label.text = $"${PlayerInventory.instance.money:0.00}";
	}
}