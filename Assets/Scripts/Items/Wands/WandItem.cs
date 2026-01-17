using UnityEngine;
using UnityEngine.UI;

public class WandItem : IItem
{
    public ItemType Type => ItemType.WandItem;

    public string Name { get => BuildWandName(); }
    public Tooltip ItemTooltip { get => BuildWandTooltip(); }
    public Image Sprite { get; }

    public readonly int Level;
    public readonly WandStats BaseStats;
    public readonly WandStats LevelStats;
    public readonly Material WandMaterial;
    public readonly WandReforge WandReforge;

    public readonly string BaseName;
    public WandStats Stats { get => CalculateStats(); }

    public WandItem(string name, int level, WandStats baseStats, WandStats levelStats, Material material, WandReforge reforge = null)
    {
        BaseName = name;
        Level = level;
        BaseStats = baseStats;
        LevelStats = levelStats;
        WandMaterial = material;
        WandReforge = reforge;
    }

    public WandItem(WandScriptableObject so, int level, Material material, WandReforge reforge = null)
    : this(so.Name, level, so.BaseStats, so.LevelStats, material, reforge) { }


    private string BuildWandName()
    {
        string name = "";
        if (WandReforge != null) name += WandReforge.Name + " ";
        name += WandMaterial.Name + " " + BaseName;
        return name;
    }


    private Tooltip BuildWandTooltip()
    {
        Tooltip tooltip = new Tooltip(Name, WandReforge != null ? GameColors.Instance.NameReforgeColor : Color.white);
        tooltip.AddLine($"Level {Level} {BaseName}", true);
        tooltip.AddNewLine();
        tooltip.AddLine($"${Stats.SellValue:0.00}", true, GameColors.Instance.GoldColor);
        tooltip.AddLine($"Power: {Stats.Power:0.00}");
        tooltip.AddLine($"Time to Cast: {Stats.TimeToCast:0.00}");
        tooltip.AddLine($"Power per Second: {Stats.PowerPerSecond:0.00}");
        tooltip.AddNewLine();
        tooltip.CombineWith(WandMaterial.MaterialTooltip);
        return tooltip;
    }

    private WandStats CalculateStats()
    {
        WandStats stats = BaseStats;
        stats += LevelStats * (Level - 1);
        stats = WandMaterial.ApplyTo(stats);
        if (WandReforge != null) WandReforge.ApplyTo(stats);
        return stats;
    }
}
