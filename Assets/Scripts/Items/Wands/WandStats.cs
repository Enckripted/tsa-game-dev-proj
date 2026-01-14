using System;

[Serializable]
public struct WandStats
{
    public double Power;
    public double TimeToCast;
    public double SellValue;

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
