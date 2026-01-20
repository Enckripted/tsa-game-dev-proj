using UnityEngine;
using System;

//Struct with operations for multiplying and adding WandStats of the same type, alongside one for
//multiplication with an integer for stuff like wand levels.
[Serializable]
public struct WandStats
{
    [field: SerializeField] public double Power { get; private set; }
    [field: SerializeField] public double TimeToCast { get; private set; }
    [field: SerializeField] public double SellValue { get; private set; }

    [field: SerializeField] public double FirePowerMult { get; private set; }
    [field: SerializeField] public double WaterPowerMult { get; private set; }
    [field: SerializeField] public double EarthPowerMult { get; private set; }
    [field: SerializeField] public double AirPowerMult { get; private set; }

    public double PowerPerSecond
    {
        get
        {
            return Power / TimeToCast;
        }
    }

    public static WandStats operator +(WandStats left, WandStats right)
    {
        left.Power += right.Power;
        left.TimeToCast -= right.TimeToCast;
        left.SellValue += right.SellValue;
        return left;
    }

    public static WandStats operator *(WandStats left, WandStats right)
    {
        left.Power *= right.Power;
        left.TimeToCast *= right.TimeToCast;
        left.SellValue *= right.SellValue;
        return left;
    }

    public static WandStats operator *(WandStats left, int times)
    {
        left.Power *= times;
        left.TimeToCast *= times;
        left.SellValue *= times;
        return left;
    }
}
