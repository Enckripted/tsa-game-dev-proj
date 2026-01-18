using UnityEngine;

//Contains the base data for every different wand type (since as of right now we have scepters and
//staffs and all that).
public class WandScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WandStats BaseStats { get; private set; }
    [field: SerializeField] public WandStats LevelStats { get; private set; }
}
