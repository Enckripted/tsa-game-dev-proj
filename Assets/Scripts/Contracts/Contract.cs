public class Contract
{
    public string Description;
    public double Reward;

    public string RequiredMaterial;
    public string RequiredBaseName;
    public double MinPower;
    public double MinSellValue;

    public bool IsSatisfied(IItem item)
    {
        if (item is not WandItem wand) return false;

        if (!string.IsNullOrEmpty(RequiredMaterial) && wand.WandMaterial.Name != RequiredMaterial) return false;
        if (!string.IsNullOrEmpty(RequiredBaseName) && wand.BaseName != RequiredBaseName) return false;

        if (wand.Stats.Power < MinPower) return false;
        if (wand.Stats.SellValue < MinSellValue) return false;

        return true;
    }

    public string GetValidationMessage(IItem item)
    {
        if (item == null) return "Enter an item";
        if (item is not WandItem wand) return "Item is not a wand";

        if (!string.IsNullOrEmpty(RequiredMaterial) && wand.WandMaterial.Name != RequiredMaterial)
            return $"Item must be made of {RequiredMaterial}";

        if (!string.IsNullOrEmpty(RequiredBaseName) && wand.BaseName != RequiredBaseName)
            return $"Item must be a {RequiredBaseName}";

        if (wand.Stats.Power < MinPower)
            return $"Power too low ({wand.Stats.Power:0}/{MinPower:0})";

        if (wand.Stats.SellValue < MinSellValue)
            return $"Value too low (${wand.Stats.SellValue:0.00}/${MinSellValue:0.00})";

        return "Ready!";
    }
}
