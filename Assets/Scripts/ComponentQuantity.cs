using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class ComponentQuantity
{
	[field: SerializeField] public string type { get; private set; }
	[field: SerializeField] public uint amount { get; private set; } //must always be positive

	public ComponentQuantity(string matType, uint matAmount)
	{
		type = matType;
		amount = matAmount;
	}
}