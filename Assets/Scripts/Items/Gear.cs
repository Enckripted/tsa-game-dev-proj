using Unity.VisualScripting;
using UnityEngine;

public class Gear : ItemData
{
	public string baseName { get; private set; }
	public GearStats baseStats { get; private set; }
	public readonly Material material;
	public readonly GearStats gearStats;

	public Gear(GearData gearData, Material nMaterial)
	{
		baseName = gearData.baseName;
		baseStats = gearData.baseStats.clone();
		material = nMaterial;
		gearStats = calcStats();
	}

	private GearStats calcStats()
	{
		GearStats stats = baseStats.clone();
		return material.apply(stats);
	}

	public override string genName()
	{
		return material.type.name + " " + baseName;
	}

	public override string genTooltip()
	{
		return
		$"Damage: {gearStats.damage}";
	}
}