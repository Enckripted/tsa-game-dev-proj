using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class ContractTileEntity : TileEntity
{
    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }

    public Inventory Inventory { get; private set; }
    public List<Contract> AvailableContracts { get; private set; } = new List<Contract>();
    public Contract AcceptedContract { get; private set; }
    public UnityEvent OnContractChanged = new UnityEvent();

    protected override void OnStart()
    {
        Inventory = new Inventory(1, true, Player.PlayerInventory);
        GenerateContracts();

    }

    public void GenerateContracts()
    {
        AvailableContracts.Clear();
        int attempts = 0;
        while (AvailableContracts.Count < 3 && attempts < 100)
        {
            Contract newContract = CreateRandomContract();
            bool duplicate = false;
            foreach (Contract c in AvailableContracts)
            {
                if (c.Description == newContract.Description)
                {
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate)
            {
                AvailableContracts.Add(newContract);
            }
            attempts++;
        }
        OnContractChanged.Invoke();
    }

    private Contract CreateRandomContract()
    {
        WandScriptableObject baseWand = ScriptableObjectData.RandomBaseWandData();
        MaterialScriptableObject material = ScriptableObjectData.RandomMaterialData();

        int targetLevel = Random.Range(1, 6);
        WandItem refWand = new WandItem(baseWand, targetLevel, new global::Material(material));

        Contract contract = new Contract();
        contract.RequiredBaseName = baseWand.Name;
        contract.RequiredMaterial = material.Name;

        double powerMult = Random.Range(0.85f, 0.95f);
        double valueMult = Random.Range(0.85f, 0.95f);

        contract.MinPower = Math.Floor(refWand.Stats.Power * powerMult);
        contract.MinSellValue = Math.Floor(refWand.Stats.SellValue * valueMult);

        contract.Reward = refWand.Stats.SellValue * 1.5f;

        string matColorHex = material.Color.ToHexString();
        string powerHex = "FF0000";
        string goldHex = GameColors.Instance.GoldColor.ToHexString();

        contract.Description = $"Create a <color=#{matColorHex}>{contract.RequiredMaterial}</color> {contract.RequiredBaseName} with at least <color=#{powerHex}>{contract.MinPower:0}</color> Power and a sell value of at least <color=yellow>${contract.MinSellValue:0.00}</color>";

        return contract;
    }

    public void AcceptContract(Contract contract)
    {
        if (AcceptedContract != null) return;
        if (!AvailableContracts.Contains(contract)) return;

        AcceptedContract = contract;

        AvailableContracts.Clear();

        OnContractChanged.Invoke();
    }

    public void CancelContract()
    {
        AcceptedContract = null;
        GenerateContracts();
    }

    public void TryFulfillContract()
    {
        if (AcceptedContract == null) return;
        if (!Inventory.SlotContainsItem(0)) return;

        IItem item = Inventory.ItemInSlot(0);
        if (AcceptedContract.IsSatisfied(item))
        {
            FulfillContract();
        }
    }

    private void FulfillContract()
    {
        Inventory.RemoveItemFromSlot(0);

        Player.AddMoney(AcceptedContract.Reward);
        Debug.Log("Contract Fulfilled! Reward: " + AcceptedContract.Reward);

        AcceptedContract = null;
        GenerateContracts();
    }

    public override void LoadUi(GameObject uiInstance)
    {
        ContractUi ui = uiInstance.GetComponent<ContractUi>();
        if (ui != null)
        {
            ui.Init(this);
        }

        Player.PlayerInventory.TargetInventory = Inventory;
    }

    public override void UnloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }
}
