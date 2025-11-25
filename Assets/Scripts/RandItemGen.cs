using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandItemGen : MonoBehaviour
{
    public static RandItemGen instance;

    public List<GearData> baseGears { get; private set; }
    public List<MaterialData> materials { get; private set; }

    T chooseFrom<T>(List<T> l)
    {
        return l[Random.Range(0, l.Count)];
    }

    public GearItem genRandomGear()
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
