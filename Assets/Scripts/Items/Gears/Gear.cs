using System.Collections.Generic;

public class Gear : ItemData
{
	public string baseName { get; private set; }
	public readonly int level;
	public readonly GearStats baseStats;
	public readonly GearStats levelStats;
	public readonly Material material;
	public readonly Reforge reforge;
	public GearStats gearStats { get; private set; }

	//we use this constructor when creating machine outputs
	public Gear(string name, int level, GearStats baseStats, GearStats levelStats, Material material, Reforge reforge = null)
	{
		this.baseName = name;
		this.level = level;
		this.baseStats = baseStats;
		this.levelStats = levelStats;
		this.material = material;
		this.reforge = reforge;
		this.gearStats = calcStats();
	}

	//and this one when loading from scriptableobject data
	public Gear(GearData data, int level, Material material, Reforge reforge = null)
	: this(data.baseName, level, data.baseStats.clone(), data.levelStats.clone(), material, reforge) { }

	private GearStats calcStats()
	{
		gearStats = baseStats + levelStats * level;
		gearStats = material.applyTo(gearStats);
		if (reforge != null) gearStats = reforge.applyTo(gearStats);
		return gearStats;
	}

	public override string genName()
	{
		List<string> words = new List<string> { material.name, baseName };
		if (reforge != null) words.Insert(0, reforge.name);
		return string.Join(" ", words);
	}

	public override string genTooltip()
	{
		List<string> lines = new List<string>() {
			$"<b>Level {level} {baseName}</b>",
			$"<b><color=#B7AD00>${gearStats.sellValue:0.00}</color></b>",
			$"Power: {gearStats.power:0.00}",
			$"Time to Cast: {gearStats.timeToCast:0.00}",
			$"Power per Second: {gearStats.powerPerSecond:0.00}\n",
			material.tooltipText+"\n",
		};
		if (reforge != null) lines.Add(reforge.tooltipText);
		return string.Join("\n", lines);
	}
}