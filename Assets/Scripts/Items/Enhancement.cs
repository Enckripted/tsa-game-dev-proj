public interface IEnhancement
{
	public string tooltipText { get; }
	public GearStats applyTo(GearStats curStats);
}