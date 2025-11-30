using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Gear : ItemData
{
	public string baseName { get; private set; }
	public readonly GearStats baseStats;
	public readonly Material material;
	public readonly Reforge reforge;
	public GearStats gearStats { get; private set; }

	//we use this constructor when creating machine outputs
	public Gear(string nName, GearStats nStats, Material nMaterial, Reforge nReforge = null)
	{
		baseName = nName;
		baseStats = nStats;
		material = nMaterial;
		reforge = nReforge;
		gearStats = calcStats();
	}

	//and this one when loading from scriptableobject data
	public Gear(GearData nData, Material nMaterial, Reforge nReforge)
	: this(nData.baseName, nData.baseStats.clone(), nMaterial, nReforge) { }

	private GearStats calcStats()
	{
		gearStats = baseStats.clone();
		gearStats = material.applyTo(gearStats);
		if (reforge != null) gearStats = reforge.applyTo(gearStats);
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
		};
		if (reforge != null) lines.Add(reforge.tooltipText);
		return string.Join("\n", lines);
	}
}