using UnityEngine;
using UnityEngine.UI;

public class GemItem : IItem, IWandEnhancement
{
    public ItemType Type => ItemType.GemItem;

    public string Name { get; }
    public Tooltip HoverTooltip { get => BuildGemTooltip(); }
    public Sprite Sprite { get; }

    public readonly WandStats StatAddition;
    public readonly WandStats StatMultiplier;

    public GemItem(GemScriptableObject so)
    {
        Name = so.Name;
        StatAddition = so.StatAddition;
        StatMultiplier = so.StatMultiplier;
        Sprite = so.Sprite;
    }

    public WandStats ApplyTo(WandStats curStats)
    {
        return (curStats + StatAddition) * StatMultiplier;
    }

    private Tooltip BuildGemTooltip()
    {
        Tooltip tooltip = new Tooltip(Name, GameColors.Instance.GemSlotColor);
        return MultiplierTooltipService.CreateFromWandStats(tooltip, StatMultiplier);
    }
}
