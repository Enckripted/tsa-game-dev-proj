using UnityEngine;

public class Gear : ItemData
{
	private readonly GearData data;

	public string baseName => data.baseName;
	public GearStats baseStats => data.baseStats;
	public readonly Material material;
	public readonly GearStats gearStats;

	public Gear(GearData gearData, Material nMaterial)
	{
		data = gearData;
		material = nMaterial;
		gearStats = calcStats();
	}

	private GearStats calcStats()
	{
		return material.apply(baseStats);
	}

	public override string genName()
	{
		return material.type.name + " " + baseName;
	}

	public override string genTooltip()
	{
		return
		$"Damage: {baseStats.damage}";
	}
}