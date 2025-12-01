using System.Collections.Generic;
using UnityEngine;

public class ComponentInventoryUi : MonoBehaviour
{
	[SerializeField] private GameObject componentQuantityElement;

	void Start()
	{
		foreach (KeyValuePair<string, uint> pair in ComponentInventory.instance.components)
		{
			GameObject nQuantity = Instantiate(componentQuantityElement, transform);
			nQuantity.GetComponent<ComponentQuantityUi>().componentType = pair.Key;
		}
	}
}