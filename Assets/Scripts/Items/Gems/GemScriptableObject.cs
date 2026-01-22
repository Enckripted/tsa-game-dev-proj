using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GemData", order = 1)]
public class GemScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WandStats StatAddition { get; private set; }
    [field: SerializeField] public WandStats StatMultiplier { get; private set; }
}
