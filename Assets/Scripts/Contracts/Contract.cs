using UnityEngine;

public class Contract
{
    public string Description { get => GetDescription(); }
    public double Reward;

    public Material RequiredMaterial;
    public string RequiredBaseName;
    public double MinPower;
    public double MinSellValue;

    private const string POWER_HEX = "FF0000";

    public bool IsSatisfied(IItem item)
    {
        if (item is not WandItem wand) return false;

        if (wand.WandMaterial.Name != RequiredMaterial.Name) return false;
        if (wand.BaseName != RequiredBaseName) return false;

        if (wand.Stats.Power < MinPower) return false;
        if (wand.Stats.SellValue < MinSellValue) return false;

        return true;
    }

    public string GetValidationMessage(IItem item)
    {
        if (item == null) return "Enter an item";
        if (item is not WandItem wand) return "Item is not a wand";

        if (wand.WandMaterial.Name != RequiredMaterial.Name)
            return $"Item must be made of {RequiredMaterial.Name}";

        if (!string.IsNullOrEmpty(RequiredBaseName) && wand.BaseName != RequiredBaseName)
            return $"Item must be a {RequiredBaseName}";

        if (wand.Stats.Power < MinPower)
            return $"Power too low ({wand.Stats.Power:0}/{MinPower:0})";

        if (wand.Stats.SellValue < MinSellValue)
            return $"Value too low (${wand.Stats.SellValue:0.00}/${MinSellValue:0.00})";

        return "Ready!";
    }

    private string GetDescription()
    {
        string description = "Create a ";
        description += $"<color=#{ColorUtility.ToHtmlStringRGB(RequiredMaterial.Color)}>{RequiredMaterial.Name}</color> ";
        description += $"{RequiredBaseName} with ";
        description += $"at least <color=#{POWER_HEX}>{MinPower:0}</color> Power";
        description += $"and a sell value of at least <color=yellow>${MinSellValue:0.00}</color>";
        return description;
    }
}
