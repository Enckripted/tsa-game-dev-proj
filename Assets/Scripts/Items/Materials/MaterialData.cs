using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MaterialData", order = 1)]
public class MaterialData : ScriptableObject
{
    public string Name;
    public Color Color;
    public WandStats StatMultipliers;
}
