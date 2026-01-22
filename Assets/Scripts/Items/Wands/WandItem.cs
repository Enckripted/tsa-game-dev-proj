using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WandItem : IItem
{
    public ItemType Type => ItemType.WandItem;

    public string Name { get => BuildWandName(); }
    public Tooltip HoverTooltip { get => BuildWandTooltip(); }
    public Image Sprite { get; }

    public readonly int Level;
    public readonly WandStats BaseStats;
    public readonly WandStats LevelStats;
    public readonly Material WandMaterial;
    public readonly WandReforge WandReforge;

    public readonly int GemSlots;
    public readonly List<GemItem> Gems;
    public int RemainingGemSlots
    {
        get
        {
            return GemSlots - Gems.Count;
        }
    }

    public readonly string BaseName;
    public WandStats Stats { get => CalculateStats(); }

    //This constructor is starting to get very big and ugly. Considering how our
    //deadline is soon though, I don't think its worth trying to make this
    //neater yet. Maybe for state tho -diego
    public WandItem(string name, int level, WandStats baseStats, WandStats levelStats, Material material, int gemSlots = 0, List<GemItem> gems = null, WandReforge reforge = null)
    {
        BaseName = name;
        Level = level;
        BaseStats = baseStats;
        LevelStats = levelStats;
        WandMaterial = material;
        WandReforge = reforge;
        GemSlots = gemSlots;

        if (gems != null) Gems = gems;
        else Gems = new List<GemItem>();
    }

    public WandItem(WandScriptableObject so, int level, Material material, int gemSlots = 0, List<GemItem> gems = null, WandReforge reforge = null)
    : this(so.Name, level, so.BaseStats, so.LevelStats, material, gemSlots, gems, reforge) { }

    public WandItem(WandItem original)
    {
        BaseName = original.BaseName;
        Level = original.Level;
        BaseStats = original.BaseStats;
        LevelStats = original.LevelStats;
        WandMaterial = original.WandMaterial.ShallowCopy();
        WandReforge = original.WandReforge != null ? original.WandReforge.ShallowCopy() : null;
        GemSlots = original.GemSlots;
        Gems = new List<GemItem>(original.Gems);
    }

    public void AddGem(GemItem gem)
    {
        if (Gems.Count == GemSlots) throw new System.Exception("Attempted to insert gem without any space");
        Gems.Add(gem);
    }

    private WandStats CalculateStats()
    {
        WandStats stats = BaseStats;
        stats += LevelStats * (Level - 1);

        stats = WandMaterial.ApplyTo(stats);
        if (WandReforge != null) WandReforge.ApplyTo(stats);

        foreach (GemItem gem in Gems)
        {
            stats = gem.ApplyTo(stats);
        }

        return stats;
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
        tooltip.CombineWith(WandMaterial.HoverTooltip);
        if (WandReforge != null)
        {
            tooltip.AddNewLine();
            tooltip.CombineWith(WandReforge.HoverTooltip);
        }
        for (int i = 0; i < GemSlots; i++)
        {
            tooltip.AddNewLine();
            if (i < Gems.Count) tooltip.CombineWith(Gems[i].HoverTooltip);
            else tooltip.AddLine("Empty Gem Slot", true, GameColors.Instance.GemSlotColor);
        }
        return tooltip;
    }

    private string BuildWandName()
    {
        string name = "";
        if (WandReforge != null) name += WandReforge.Name + " ";
        name += WandMaterial.Name + " " + BaseName;
        return name;
    }
}
