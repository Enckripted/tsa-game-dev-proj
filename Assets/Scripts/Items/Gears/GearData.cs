using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseGearData", order = 1)]
[Serializable]
public class GearData : ScriptableObject
{
	[field: SerializeField] public string baseName { get; private set; }
	[field: SerializeField] public GearStats baseStats { get; private set; }
}