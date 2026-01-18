public class WandReforge : IWandEnhancement
{
    public readonly string Name;
    public readonly WandStats StatMultipliers;

    public Tooltip HoverTooltip
    {
        get
        {
            Tooltip tooltip = new Tooltip();
            tooltip.AddLine(Name, true, GameColors.Instance.NameReforgeColor);
            tooltip.AddLine($"x{StatMultipliers.Power:0.00} to item power");
            tooltip.AddLine($"x{StatMultipliers.TimeToCast:0.00} to casting speed");
            return tooltip;
        }
    }

    public WandReforge(WandReforgeScriptableObject so)
    {
        Name = so.Name;
        StatMultipliers = so.StatMultiplier;
    }

    public WandStats ApplyTo(WandStats curStats)
    {
        return curStats * StatMultipliers;
    }
}
