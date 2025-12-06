using System.Collections.Generic;
using UnityEngine;

public class RandomItemFactory : MonoBehaviour
{
    public static RandomItemFactory instance { get; private set; }

    public List<GearData> baseGears { get; private set; }
    public List<MaterialData> materials { get; private set; }
    public List<ReforgeData> reforges { get; private set; }

    T chooseFrom<T>(List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    public GearItem createRandomItem()
    {
        GearData gearData = chooseFrom(baseGears);
        MaterialData materialData = chooseFrom(materials);
        ReforgeData reforgeData = chooseFrom(reforges);
        Gear gear = new Gear(gearData, 1,
            new Material(materialData),
            Random.Range(1, 10) == 1 ? new Reforge(reforgeData) : null);
        return new GearItem(gear);
    }

    void Awake()
    {
        instance = this;
        baseGears = new List<GearData>(Resources.LoadAll<GearData>("Base Gears"));
        materials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
        reforges = new List<ReforgeData>(Resources.LoadAll<ReforgeData>("Reforges"));
    }
}
