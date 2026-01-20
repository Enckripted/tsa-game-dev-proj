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
            Tooltip tooltip = new Tooltip(Name, Color);
            return MultiplierTooltipService.CreateFromWandStats(tooltip, StatMultiplier);
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

    public Material ShallowCopy()
    {
        return (Material)MemberwiseClone();
    }
}

