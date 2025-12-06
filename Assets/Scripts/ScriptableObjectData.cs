using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectData : MonoBehaviour
{
	public static ScriptableObjectData instance;

	public List<GearData> baseGears { get; private set; }
	public List<MaterialData> materials { get; private set; }
	public List<ReforgeData> reforges { get; private set; }

	//i don't know if there's a better way of doing this
	T chooseFrom<T>(List<T> l)
	{
		return l[Random.Range(0, l.Count)];
	}
	public GearData getRandomGearData()
	{
		return chooseFrom(baseGears);
	}

	public Material getRandomMaterial()
	{
		return new Material(chooseFrom(materials));
	}

	public Reforge getRandomReforge()
	{
		return new Reforge(chooseFrom(reforges));
	}

	void Awake()
	{
		instance = this;
		baseGears = new List<GearData>(Resources.LoadAll<GearData>("Base Gears"));
		materials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
		reforges = new List<ReforgeData>(Resources.LoadAll<ReforgeData>("Reforges"));
	}
}