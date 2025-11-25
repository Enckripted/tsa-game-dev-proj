using System;

[Serializable]
public class GearStats
{
	public double damage;
	public double sellValue;

	public GearStats clone()
	{
		return (GearStats)MemberwiseClone();
	}
}