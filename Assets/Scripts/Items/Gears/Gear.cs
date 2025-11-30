using System.Collections.Generic;

public class Gear : ItemData
{
	public string baseName { get; private set; }
	public readonly GearStats baseStats;
	public readonly Material material;
	public readonly Reforge reforge;
	public GearStats gearStats { get; private set; }

	public Gear(GearData gearData, Material nMaterial, Reforge nReforge)
	{
		baseName = gearData.baseName;
		baseStats = gearData.baseStats.clone();
		material = nMaterial;
		reforge = nReforge;
		gearStats = calcStats();
	}

	private GearStats calcStats()
	{
		gearStats = baseStats.clone();
		gearStats = material.applyTo(gearStats);
		gearStats = reforge.applyTo(gearStats);
		return gearStats;
	}

	public override string genName()
	{
		return $"{reforge.name} {material.name} {baseName}";
	}

	public override string genTooltip()
	{
		List<string> lines = new List<string>() {
			$"<b>{baseName}</b>",
			$"<b><color=#B7AD00>${gearStats.sellValue:0.00}</color></b>",
			$"Power: {gearStats.power:0.00}",
			$"Time to Cast: {gearStats.timeToCast:0.00}",
			$"Power per Second: {gearStats.powerPerSecond:0.00}\n",
			material.tooltipText+"\n",
			reforge.tooltipText,
		};
		return string.Join("\n", lines);
	}
}