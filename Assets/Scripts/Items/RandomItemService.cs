using System.Collections.Generic;
using UnityEngine;

public class RandomItemService : MonoBehaviour
{
    [field: SerializeField] protected int ChanceOfReforge { get; private set; }

    [field: SerializeField] protected int MaxGemSlots { get; private set; }
    [field: SerializeField] protected int ChanceOfGemSlot { get; private set; }

    [field: SerializeField] protected int ChanceOfGem { get; private set; }

    private static RandomItemService instance;

    public static WandItem CreateRandomWand()
    {
        WandScriptableObject wandData = ScriptableObjectData.RandomBaseWandData();
        MaterialScriptableObject materialData = ScriptableObjectData.RandomMaterialData();
        WandReforgeScriptableObject reforgeData = ScriptableObjectData.RandomWandReforgeData();

        int gemSlots = 0;
        for (int i = 0; i < instance.MaxGemSlots; i++)
        {
            if (Random.Range(0, instance.ChanceOfGemSlot) != 0) continue;
            gemSlots++;
        }

        List<GemItem> gems = new List<GemItem>();
        for (int i = 0; i < gemSlots; i++)
        {
            if (Random.Range(0, instance.ChanceOfGem) != 0) continue;
            GemScriptableObject gemData = ScriptableObjectData.RandomGemData();
            gems.Add(new GemItem(gemData));
        }

        WandItem wand = new WandItem(wandData, 1,
            new Material(materialData),
            gemSlots,
            gems,
            Random.Range(0, 10) == 0 ? new WandReforge(reforgeData) : null);
        return wand;
    }

    void Awake()
    {
        instance = this;
    }
}
