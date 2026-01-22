public class WandReforge : IWandEnhancement
{
    public readonly string Name;
    public readonly WandStats StatMultiplier;

    public Tooltip HoverTooltip
    {
        get
        {
            Tooltip tooltip = new Tooltip(Name, GameColors.Instance.NameReforgeColor);
            return MultiplierTooltipService.CreateFromWandStats(tooltip, StatMultiplier);
        }
    }

    public WandReforge(WandReforgeScriptableObject so)
    {
        Name = so.Name;
        StatMultiplier = so.StatMultiplier;
    }

    public WandStats ApplyTo(WandStats curStats)
    {
        return curStats * StatMultiplier;
    }

    public WandReforge ShallowCopy()
    {
        return (WandReforge)MemberwiseClone();
    }
}
