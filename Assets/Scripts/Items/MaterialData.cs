using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MaterialData", order = 1)]
public class MaterialData : ScriptableObject
{
	public string baseName;
	public Color color;
	public MaterialStats stats;
}