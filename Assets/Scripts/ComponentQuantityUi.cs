using TMPro;
using UnityEngine;

public class ComponentQuantityUi : MonoBehaviour
{
	public string componentType { get; set; }

	private TextMeshProUGUI textElement;

	void Awake()
	{
		textElement = GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		textElement.text = $"{componentType}: {ComponentInventory.instance.getQuantity(componentType)}";
	}
}