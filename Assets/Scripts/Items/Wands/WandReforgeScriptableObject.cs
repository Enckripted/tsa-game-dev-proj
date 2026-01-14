using UnityEngine;

public class WandReforgeScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WandStats StatMultiplier { get; private set; }
}
