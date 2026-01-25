using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using System.Linq;

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
                Debug.Log("Added contract " + (AvailableContracts.Count + 1) + " with difficulty " + newContract.Difficulty);
                AvailableContracts.Add(newContract);
            }
            attempts++;
        }
        OnContractChanged.Invoke();
    }

    private Contract LegacyCreateRandomContract()
    {
        WandScriptableObject baseWand = ScriptableObjectData.RandomBaseWandData();
        MaterialScriptableObject material = ScriptableObjectData.RandomMaterialData();

        int targetLevel = Random.Range(1, 6);
        WandItem refWand = new WandItem(baseWand, targetLevel, new global::Material(material));

        Contract contract = new Contract();
        contract.RequiredBaseName = baseWand.Name;
        contract.RequiredMaterial = new Material(material);

        double powerMult = Random.Range(0.85f, 0.95f);
        double valueMult = Random.Range(0.85f, 0.95f);

        contract.MinPower = Math.Floor(refWand.Stats.Power * powerMult);
        contract.MinSellValue = Math.Floor(refWand.Stats.SellValue * valueMult);

        contract.Reward = refWand.Stats.SellValue * 1.5f;

        return contract;
    }

    private Contract CreateRandomContract()
    {
        Contract contract = new Contract();

        //we will always force a specific type of wand
        WandScriptableObject baseWand = ScriptableObjectData.RandomBaseWandData();
        WandStats baseLevelStats = baseWand.LevelStats;
        contract.RequiredBaseName = baseWand.Name;

        //and we'll test other attributes (in order): material, power, time to cast, sell value
        bool[] attributesToExamine = new bool[4];

        //force at least one attribute to be enabled
        attributesToExamine[Random.Range(0, attributesToExamine.Length)] = true;
        //now have a chance of enabling more of them
        int attributesEnabled = 1;
        for (int i = 0; i < attributesToExamine.Length; i++)
        {
            if (attributesToExamine[i] || attributesEnabled >= 3) continue;
            if (Random.Range(1, 4) == 1)
            {
                attributesToExamine[i] = true;
                attributesEnabled++;
            }
        }

        double difficulty = 0;
        if (attributesToExamine[0])
        {
            MaterialScriptableObject materialData = ScriptableObjectData.RandomMaterialData();

            contract.RequiredMaterial = new Material(materialData);
            difficulty += 1;
        }
        if (attributesToExamine[1])
        {
            int level = Random.Range(3, 9);
            float randMult = Random.Range(0.75f, 1.25f);

            contract.MinPower = baseLevelStats.Power * (level - 3) * randMult;
            difficulty += level * randMult;
        }
        if (attributesToExamine[2])
        {
            int level = Random.Range(1, 5);
            float randMult = Random.Range(0.75f, 1.25f);

            contract.MaxTimeToCast = 2.4 - baseLevelStats.TimeToCast * level * randMult;
            difficulty += level * randMult;
        }
        if (attributesToExamine[3])
        {
            int level = Random.Range(1, 6);
            float randMult = Random.Range(0.75f, 1.25f);

            contract.MinSellValue = baseLevelStats.SellValue * level * randMult;
            difficulty += level * randMult;
        }
        difficulty *= attributesEnabled / 3.0; //we need a double or otherwise 1/2 = 0!

        contract.Difficulty = difficulty;
        contract.Reward = Math.Pow(difficulty, 1.25);

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

    //im sorry for doing this nikhil
    public void OverrideContracts(List<Contract> contracts)
    {
        AvailableContracts = contracts;
        OnContractChanged.Invoke();
    }

    private void FulfillContract()
    {
        Inventory.RemoveItemFromSlot(0);
        Player.ContractsCompleted++;
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
