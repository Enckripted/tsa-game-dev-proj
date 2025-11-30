using System;
using System.Collections.Generic;
using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using UnityEngine;

[Serializable]
public class Material : IEnhancement
{
	public readonly string name;
	public readonly Color color;
	[SerializeField] private readonly MaterialStats stats;

	public string tooltipText
	{
		get
		{
			//TODO: consolidate this logic into some sort of TooltipBuilder
			List<string> lines = new List<string>() { $"<b><color={color.ToHex()}>{name}</color></b>" };
			if (stats.powerMult != 1.0) lines.Add($"x{stats.powerMult:0.00} to item power");
			if (stats.castTimeMult != 1.0) lines.Add($"x{stats.castTimeMult:0.00} to casting speed");
			if (stats.sellMult != 1.0) lines.Add($"x{stats.sellMult:0.00} to sell value");
			return string.Join("\n", lines);
		}
	}

	public Material(MaterialData materialData)
	{
		name = materialData.baseName;
		color = materialData.color;
		stats = materialData.stats.clone();
	}

	public GearStats applyTo(GearStats curStats)
	{
		curStats.power *= stats.powerMult;
		curStats.timeToCast *= stats.castTimeMult;
		curStats.sellValue *= stats.sellMult;
		return curStats;
	}
}

