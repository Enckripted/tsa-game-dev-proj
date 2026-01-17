using UnityEngine;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

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
}

public class ContractTileEntity : TileEntity
{
	[field: SerializeField] public override GameObject uiPrefab { get; protected set; }
    
    public Inventory Inventory { get; private set; }
    public Contract CurrentContract { get; private set; }
    
    public UnityEvent OnContractChanged = new UnityEvent();

	protected override void onStart()
	{
		Inventory = new Inventory(1, Player.PlayerInventory);
        GenerateNewContract();
        
        Inventory.Changed.AddListener(CheckContract);
	}

    public void GenerateNewContract()
    {
        WandScriptableObject baseWand = ScriptableObjectData.RandomBaseWandData();
        MaterialData material = ScriptableObjectData.RandomMaterialData();
        
        int targetLevel = Random.Range(1, 4);
        WandItem refWand = new WandItem(baseWand, targetLevel, new global::Material(material));
        
        CurrentContract = new Contract();
        CurrentContract.RequiredBaseName = baseWand.Name;
        CurrentContract.RequiredMaterial = material.Name;
        
        CurrentContract.MinPower = Math.Floor(refWand.Stats.Power * 0.9f);
        CurrentContract.MinSellValue = Math.Floor(refWand.Stats.SellValue * 0.9f);
        
        CurrentContract.Reward = refWand.Stats.SellValue * 1.5f;

        string matColorHex = material.Color.ToHexString();
        string powerHex = "FF0000";
        string goldHex = GameColors.Instance.GoldColor.ToHexString();
        
        CurrentContract.Description = $"Create a <color=#{matColorHex}>{CurrentContract.RequiredMaterial}</color> {CurrentContract.RequiredBaseName} with at least <color=#{powerHex}>{CurrentContract.MinPower:0}</color> Power and a sell value of at least <color=#{goldHex}>${CurrentContract.MinSellValue:0.00}</color>";
        
        OnContractChanged.Invoke();
    }

    public void CheckContract()
    {
        if (!Inventory.SlotContainsItem(0)) return;
        
        IItem item = Inventory.ItemInSlot(0);
        if (CurrentContract.IsSatisfied(item))
        {
            FulfillContract();
        }
    }

    private void FulfillContract()
    {
        Inventory.RemoveItemFromSlot(0); 
        
        Player.AddMoney(CurrentContract.Reward);

        GenerateNewContract();
    }

	public override void loadUi(GameObject uiInstance)
	{
        ContractUi ui = uiInstance.GetComponent<ContractUi>();
        if (ui != null)
        {
            ui.Init(this);
        }
        
        Player.PlayerInventory.TargetInventory = Inventory;
	}

	public override void unloadUi(GameObject uiInstance)
	{
        Player.PlayerInventory.TargetInventory = null;
	}
}