using System;
using UnityEngine;

[Serializable]
public class MaterialStats
{
	[field: SerializeField] public double powerMult { get; private set; }
	[field: SerializeField] public double castTimeMult { get; private set; }
	[field: SerializeField] public double sellMult { get; private set; }

	public MaterialStats clone()
	{
		return (MaterialStats)MemberwiseClone();
	}
}
