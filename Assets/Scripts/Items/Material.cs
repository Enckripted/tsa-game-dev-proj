using System;
using UnityEngine;

[Serializable]
public class Material : Enhancement
{
	public MaterialType type;
	public double damageMult;

	override public GearStats apply(GearStats curStats)
	{
		curStats.damage *= damageMult;
		return curStats;
	}
}

[Serializable]
public class MaterialType
{
	public String name;
	public Color color;
}