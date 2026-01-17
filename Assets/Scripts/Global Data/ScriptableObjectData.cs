using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectData : MonoBehaviour
{
    public static List<WandScriptableObject> BaseWands { get; private set; }
    public static List<MaterialData> BaseMaterials { get; private set; }
    public static List<WandReforgeScriptableObject> BaseWandReforges { get; private set; }

    //i don't know if there's a better way of doing this
    private static T ChooseFrom<T>(List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    public static WandScriptableObject RandomBaseWandData()
    {
        return ChooseFrom(BaseWands);
    }

    public static MaterialData RandomMaterialData()
    {
        return ChooseFrom(BaseMaterials);
    }

    public static WandReforgeScriptableObject RandomWandReforgeData()
    {
        return ChooseFrom(BaseWandReforges);
    }

    void Awake()
    {
        BaseWands = new List<WandScriptableObject>(Resources.LoadAll<WandScriptableObject>("Base Wands"));
        BaseMaterials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
        BaseWandReforges = new List<WandReforgeScriptableObject>(Resources.LoadAll<WandReforgeScriptableObject>("Wand Reforges"));
    }
}
