using System.Collections.Generic;
using UnityEngine;

public static class RandomItemService
{
    public static WandItem CreateRandomWand()
    {
        WandScriptableObject wandData = ScriptableObjectData.RandomBaseWandData();
        MaterialData materialData = ScriptableObjectData.RandomMaterialData();
        WandReforgeScriptableObject reforgeData = ScriptableObjectData.RandomWandReforgeData();
        WandItem wand = new WandItem(wandData, 1,
            new Material(materialData),
            Random.Range(1, 10) == 1 ? new WandReforge(reforgeData) : null);
        return wand;
    }
}
