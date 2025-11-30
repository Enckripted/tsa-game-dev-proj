using System;

[Serializable]
public class GearStats
{
	public double power;
	public double timeToCast;
	public double sellValue;

	public double powerPerSecond
	{
		get
		{
			return power / timeToCast;
		}
	}

	public GearStats clone()
	{
		return (GearStats)MemberwiseClone();
	}
}