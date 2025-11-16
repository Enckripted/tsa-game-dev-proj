using System;
using UnityEngine;

[Serializable]
public class Material : Enhancement
{
	private readonly MaterialData data;
	public MaterialType type => data.type;

	public Material(MaterialData materialData)
	{
		data = materialData;
	}

	override public GearStats apply(GearStats curStats)
	{
		curStats.damage *= data.damageMult;
		curStats.sellValue *= data.sellMult;
		return curStats;
	}
}

[Serializable]
public class MaterialType
{
	public String name;
	public Color color;
}

