using System.Collections.Generic;
using UnityEngine;

public class ComponentListUi : MonoBehaviour
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