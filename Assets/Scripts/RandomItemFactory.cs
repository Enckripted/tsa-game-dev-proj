using System.Collections.Generic;
using UnityEngine;

public class RandomItemFactory : MonoBehaviour
{
    public static RandomItemFactory instance { get; private set; }

    public List<GearData> baseGears { get; private set; }
    public List<MaterialData> materials { get; private set; }

    T chooseFrom<T>(List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    public GearItem createRandomItem()
    {
        GearData gearData = chooseFrom(baseGears);
        MaterialData materialData = chooseFrom(materials);
        Gear gear = new Gear(gearData, new Material(materialData));
        return new GearItem(gear);
    }

    void Awake()
    {
        instance = this;
        baseGears = new List<GearData>(Resources.LoadAll<GearData>("Base Gears"));
        materials = new List<MaterialData>(Resources.LoadAll<MaterialData>("Materials"));
    }
}
