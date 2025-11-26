using System;
using UnityEngine;

[Serializable]
public class Material : Enhancement
{
	public readonly string name;
	public readonly Color color;
	[SerializeField] private readonly MaterialStats stats;

	public Material(MaterialData materialData)
	{
		name = materialData.baseName;
		color = materialData.color;
		stats = materialData.stats.clone();
	}

	override public GearStats apply(GearStats curStats)
	{
		curStats.damage *= stats.damageMult;
		curStats.sellValue *= stats.sellMult;
		return curStats;
	}
}

