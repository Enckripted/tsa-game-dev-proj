using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ReforgeData", order = 2)]
public class ReforgeData : ScriptableObject
{
	public string baseName;
	public ReforgeStats stats;
}