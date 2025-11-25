using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ComponentInventory : MonoBehaviour
{
	public static ComponentInventory instance;

	//as far as i know there's no real downside to using serializeddictionary instead of a normal dictionary
	//so im just gonna keep this here
	[field: SerializeField] public SerializedDictionary<string, uint> components { get; private set; }

	private Exception notExistsException(string matType)
	{
		return new Exception("No existing component type called " + matType);
	}

	private bool matTypeExists(string matType)
	{
		return components.ContainsKey(matType);
	}

	public bool hasQuantityAvailable(ComponentQuantity compQuant)
	{
		return components[compQuant.type] >= compQuant.amount;
	}

	public bool hasQuantitiesAvailable(IEnumerable<ComponentQuantity> compQuants)
	{
		foreach (ComponentQuantity compQuant in compQuants)
		{
			if (!hasQuantityAvailable(compQuant)) return false;
		}
		return true;
	}

	public uint getQuantity(string matType)
	{
		if (!matTypeExists(matType)) throw notExistsException(matType);
		return components[matType];
	}

	public void addComponentQuantity(ComponentQuantity compQuant)
	{
		if (!matTypeExists(compQuant.type)) throw notExistsException(compQuant.type);
		components[compQuant.type] += compQuant.amount;
	}

	public void subtractComponentQuantity(ComponentQuantity compQuant)
	{
		if (!matTypeExists(compQuant.type)) throw notExistsException(compQuant.type);
		if (getQuantity(compQuant.type) - compQuant.amount < 0) throw new Exception("Subtracted more than current amount for component type called " + compQuant.type);
		components[compQuant.type] -= compQuant.amount;
	}

	void Awake()
	{
		instance = this;

		List<MaterialData> materials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
		foreach (MaterialData material in materials)
		{
			components.Add(material.name, 0);
		}
	}
}