using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseGearData", order = 1)]
public class GearData : ScriptableObject
{
	public string baseName;
	public GearStats baseStats;
}