using System;
using Unity.VisualScripting;

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

	public static GearStats operator +(GearStats left, GearStats right)
	{
		GearStats ret = left.clone();
		ret.power += right.power;
		ret.timeToCast -= right.timeToCast;
		ret.sellValue += right.sellValue;
		return ret;
	}

	public static GearStats operator *(GearStats left, int times)
	{
		GearStats ret = left.clone();
		ret.power *= times;
		ret.timeToCast *= times;
		ret.sellValue *= times;
		return ret;
	}

	public GearStats clone()
	{
		return (GearStats)MemberwiseClone();
	}
}