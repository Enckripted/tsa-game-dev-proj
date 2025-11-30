using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reforge : IEnhancement
{
	[SerializeField] public readonly string name;
	[SerializeField] private readonly ReforgeStats stats;

	public string tooltipText
	{
		get
		{
			List<string> lines = new List<string>() { $"<b><color=#2e8b57>{name}</color></b>" };
			if (stats.powerMult != 1.0) lines.Add($"x{stats.powerMult:0.00} to item power");
			if (stats.castTimeMult != 1.0) lines.Add($"x{stats.castTimeMult:0.00} to casting speed");
			return string.Join("\n", lines);
		}
	}

	public Reforge(ReforgeData reforgeData)
	{
		name = reforgeData.baseName;
		stats = reforgeData.stats.clone();
	}

	public GearStats applyTo(GearStats curStats)
	{
		curStats.power *= stats.powerMult;
		curStats.timeToCast *= stats.castTimeMult;
		return curStats;
	}
}