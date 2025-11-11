using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MaterialData", order = 1)]
public class MaterialData : ScriptableObject
{
	public MaterialType type;
	public double damageMult = 1;
	public double sellMult = 1;
}