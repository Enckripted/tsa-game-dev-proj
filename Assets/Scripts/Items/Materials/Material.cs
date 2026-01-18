using System;
using UnityEngine;

//Basic wand enhancement implementation. Created from MaterialScriptableObject, and just applies a
//bunch of stat multipliers.
[Serializable]
public class Material : IWandEnhancement
{
    public readonly string Name;
    public readonly Color Color;
    private readonly WandStats StatMultiplier;

    public Tooltip HoverTooltip
    {
        get
        {
            Tooltip tooltip = new Tooltip();
            tooltip.AddLine(Name, true, Color);
            if (StatMultiplier.Power != 1.0) tooltip.AddLine($"x{StatMultiplier.TimeToCast:0.00} to item power");
            if (StatMultiplier.TimeToCast != 1.0) tooltip.AddLine($"x{StatMultiplier.TimeToCast:0.00} to casting speed");
            if (StatMultiplier.SellValue != 1.0) tooltip.AddLine($"x{StatMultiplier.SellValue:0.00} to sell value");
            return tooltip;
        }
    }

    public Material(MaterialScriptableObject materialData)
    {
        Name = materialData.Name;
        Color = materialData.Color;
        StatMultiplier = materialData.StatMultipliers;
    }

    public WandStats ApplyTo(WandStats curStats)
    {
        return curStats * StatMultiplier;
    }
}

