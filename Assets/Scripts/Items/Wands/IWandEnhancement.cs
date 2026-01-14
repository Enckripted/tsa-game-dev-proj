public interface IWandEnhancement
{
    public Tooltip HoverTooltip { get; }
    public WandStats ApplyTo(WandStats curStats);
}
