//Tooltip to be displayed alongside the item + apply function.
public interface IWandEnhancement
{
    public Tooltip HoverTooltip { get; }
    public WandStats ApplyTo(WandStats curStats);
}
