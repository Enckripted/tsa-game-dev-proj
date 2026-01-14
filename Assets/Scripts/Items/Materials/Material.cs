using System;
using System.Collections.Generic;
using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using UnityEngine;

[Serializable]
public class Material
{
    public readonly string Name;
    public readonly Color Color;
    private readonly WandStats StatMultiplier;

    public Tooltip MaterialTooltip
    {
        get
        {
            Tooltip tooltip = new Tooltip();
            tooltip.AddLine(Name, true, Color.ToHex());
            if (StatMultiplier.Power != 1.0) tooltip.AddLine($"x{StatMultiplier.TimeToCast:0.00} to item power");
            if (StatMultiplier.TimeToCast != 1.0) tooltip.AddLine($"x{StatMultiplier.TimeToCast:0.00} to casting speed");
            if (StatMultiplier.SellValue != 1.0) tooltip.AddLine($"x{StatMultiplier.SellValue:0.00} to sell value");
            return tooltip;
        }
    }

    public Material(MaterialData materialData)
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

