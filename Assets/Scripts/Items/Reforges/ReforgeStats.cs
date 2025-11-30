using System;
using UnityEngine;

[Serializable]
public class ReforgeStats
{
	[field: SerializeField] public double powerMult;
	[field: SerializeField] public double castTimeMult;

	public ReforgeStats clone()
	{
		return (ReforgeStats)MemberwiseClone();
	}
}