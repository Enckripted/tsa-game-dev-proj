using System.Collections.Generic;
using UnityEngine;

public class CuttingMachine : BaseMachine
{
    public override int NumInputSlots => 12;
    public override int NumOutputSlots => 12;
    public override bool RunsAutomatically => false;
    public override bool StopsWhenFinished => false;

    [field: SerializeField] public double RecipeDuration { get; private set; }
    [field: SerializeField] public int CutGemChance { get; private set; }

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    private InventorySlot FindSlotContainingValidWand()
    {
        foreach (InventorySlot slot in InputSlots)
            if (slot.ContainsItem && slot.StoredItem.Type == ItemType.WandItem && (slot.StoredItem as WandItem).Gems.Count > 0) return slot;
        return null;
    }

    public override bool HasValidRecipe()
    {
        return FindSlotContainingValidWand() != null;
    }

    protected override Recipe GetRecipe()
    {
        WandItem wand = FindSlotContainingValidWand().StoredItem as WandItem;

        List<GemItem> gems = new List<GemItem>();
        foreach (GemItem gem in wand.Gems)
            if (Random.Range(0, CutGemChance) == 0) gems.Add(gem);

        return new Recipe(RecipeDuration, new FragmentInventory(), new FragmentInventory(), gems);
    }

    protected override void ExtractItemInputs()
    {
        FindSlotContainingValidWand().Pop();
    }

    protected override void OnRecipeEnd() { }
    protected override void MachineUpdate() { }
    protected override void LoadMachineIntoUi(GameObject uiInstance)
    {
        CuttingMachineUi ui = uiInstance.GetComponent<CuttingMachineUi>();
        ui.Machine = this;
    }
}
