using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ComponentInventory : MonoBehaviour
{
	public static ComponentInventory instance;

	public Dictionary<string, int> components { get; private set; }

	private bool matTypeExists(String matType)
	{
		return components.ContainsKey(matType);
	}

	public void addComponentQuantity(ComponentQuantity compQuant)
	{
		if (!matTypeExists(compQuant.type)) throw new Exception("No existing component type called " + compQuant.type);
		components[compQuant.type] += compQuant.amount;
	}

	public void subtract

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