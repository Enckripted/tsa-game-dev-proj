//Noticed while implementing gems that I was going to be repeating myself a lot
//so I decided to make this -diego
public static class MultiplierTooltipService
{
    public static Tooltip CreateFromWandStats(Tooltip original, WandStats multiplier)
    {
        if (multiplier.Power != 1.0) original.AddLine($"x{multiplier.Power:0.00} to item power");
        if (multiplier.TimeToCast != 1.0) original.AddLine($"x{multiplier.TimeToCast:0.00} to casting speed");
        if (multiplier.SellValue != 1.0) original.AddLine($"x{multiplier.SellValue:0.00} to sell value");

        if (multiplier.FirePowerMult != 1.0) original.AddLine($"x{multiplier.FirePowerMult:0.00} to Fire Power");
        if (multiplier.WaterPowerMult != 1.0) original.AddLine($"x{multiplier.WaterPowerMult:0.00} to Fire Power");
        if (multiplier.EarthPowerMult != 1.0) original.AddLine($"x{multiplier.EarthPowerMult:0.00} to Earth Power");
        if (multiplier.AirPowerMult != 1.0) original.AddLine($"x{multiplier.AirPowerMult:0.00} to Air Power");

        return original;
    }
}
