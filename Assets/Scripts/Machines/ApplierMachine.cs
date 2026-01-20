using System.Collections.Generic;
using UnityEngine;

public class ApplierMachine : BaseMachine
{
    public override int NumInputSlots => 12;
    public override int NumOutputSlots => 12;
    public override bool RunsAutomatically => false;
    public override bool StopsWhenFinished => false;

    [field: SerializeField] public double RecipeDuration { get; private set; }

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    private InventorySlot FindSlotContainingValidWand()
    {
        foreach (InventorySlot slot in InputSlots)
        {
            if (!slot.ContainsItem || slot.StoredItem.Type != ItemType.WandItem) continue;
            WandItem wand = slot.StoredItem as WandItem;
            if (wand.GemSlots - wand.Gems.Count > 0) return slot;
        }
        return null;
    }

    private List<InventorySlot> FindSlotsWithGems()
    {
        List<InventorySlot> res = new List<InventorySlot>();
        foreach (InventorySlot slot in InputSlots)
            if (slot.ContainsItem && slot.StoredItem.Type == ItemType.GemItem) res.Add(slot);
        return res;
    }


    public override bool HasValidRecipe()
    {
        return FindSlotContainingValidWand() != null && FindSlotsWithGems().Count > 0;
    }

    protected override Recipe GetRecipe()
    {
        WandItem wand = new WandItem(FindSlotContainingValidWand().StoredItem as WandItem);
        foreach (InventorySlot slot in FindSlotsWithGems())
            if (wand.RemainingGemSlots > 0) wand.AddGem(slot.StoredItem as GemItem);

        return new Recipe(RecipeDuration, new FragmentInventory(), new FragmentInventory(), new List<IItem> { wand });
    }

    protected override void ExtractItemInputs()
    {
        WandItem wand = FindSlotContainingValidWand().StoredItem as WandItem;
        int gemsToRemove = wand.RemainingGemSlots;
        foreach (InventorySlot slot in FindSlotsWithGems())
        {
            if (gemsToRemove > 0)
            {
                slot.Pop();
                gemsToRemove -= 1;
            }
        }
        FindSlotContainingValidWand().Pop();
    }

    protected override void OnRecipeEnd() { }
    protected override void MachineUpdate() { }
    protected override void LoadMachineIntoUi(GameObject uiInstance)
    {
        ApplierMachineUi ui = uiInstance.GetComponent<ApplierMachineUi>();
        ui.Machine = this;
    }
}
