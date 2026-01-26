using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Contract
{
    public string Description { get => GetDescription(); }
    public double Difficulty;
    public double Reward;

    public Material RequiredMaterial = null;
    public string RequiredBaseName = "";
    public double MinPower = 0;
    public double MaxTimeToCast = 0;
    public double MinSellValue = 0;

    private const string POWER_HEX = "FF0000";

    public bool IsSatisfied(IItem item)
    {
        if (item is not WandItem wand) return false;

        if (RequiredMaterial != null && wand.WandMaterial.Name != RequiredMaterial.Name) return false;
        if (wand.BaseName != RequiredBaseName) return false;

        if (MinPower != 0 && wand.Stats.Power < MinPower) return false;
        if (MaxTimeToCast != 0 && wand.Stats.TimeToCast > MaxTimeToCast) return false;
        if (MinSellValue != 0 && wand.Stats.SellValue < MinSellValue) return false;

        return true;
    }

    public string GetValidationMessage(IItem item)
    {
        if (item == null) return "Enter an item";
        if (item is not WandItem wand) return "Item is not a wand";

        if (RequiredMaterial != null && wand.WandMaterial.Name != RequiredMaterial.Name)
            return $"Item must be made of {RequiredMaterial.Name}";

        if (!string.IsNullOrEmpty(RequiredBaseName) && wand.BaseName != RequiredBaseName)
            return $"Item must be a {RequiredBaseName}";

        if (MinPower != 0 && wand.Stats.Power < MinPower)
            return $"Power too low ({wand.Stats.Power:0}/{MinPower:0})";

        if (MaxTimeToCast != 0 && wand.Stats.TimeToCast > MaxTimeToCast)
            return "Time to cast too high ({wand.Stats.TimeToCast}/{MaxTimeToCast})";

        if (MinSellValue != 0 && wand.Stats.SellValue < MinSellValue)
            return $"Value too low (${wand.Stats.SellValue:0.00}/${MinSellValue:0.00})";

        return "Ready!";
    }

    private string GetDescription()
    {
        List<string> description = new List<string>();

        if (MinPower != 0)
            description.Add($"at least <color=#{POWER_HEX}>{MinPower:0}</color> Power");
        if (MaxTimeToCast != 0)
            description.Add($"a max Time to Cast of <color=blue>{MaxTimeToCast:0.00}</color> secs");
        if (MinSellValue != 0)
            description.Add($"a Sell Value of at least <color=yellow>${MinSellValue:0.00}</color>");

        if (description.Count > 1)
            description[description.Count - 1] = "and " + description[description.Count - 1];

        return
            "Create a " + (RequiredMaterial != null ? $"<color=#{ColorUtility.ToHtmlStringRGB(RequiredMaterial.Color)}>{RequiredMaterial.Name}</color> " : "") +
            RequiredBaseName +
            (description.Count > 0 ? " with " : "") +
            (description.Count > 2 ? string.Join(", ", description) : string.Join(" ", description));
    }
}
