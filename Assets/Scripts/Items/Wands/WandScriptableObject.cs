using UnityEngine;

public class WandScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WandStats BaseStats { get; private set; }
    [field: SerializeField] public WandStats LevelStats { get; private set; }
}
