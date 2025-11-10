public class Gear : ItemData
{
	public string baseName;
	public readonly GearStats baseStats;
	public readonly Material material;
	public readonly GearStats gearStats;

	private GearStats calcStats()
	{
		return material.apply(baseStats);
	}

	public Gear(GearStats nBaseStats, Material nMaterial)
	{
		baseStats = nBaseStats;
		material = nMaterial;
		gearStats = calcStats();
	}

	public override string genName()
	{
		return material.type.name + baseName;
	}

	public override string genTooltip()
	{
		return
		$"Damage: {baseStats.damage}";
	}
}