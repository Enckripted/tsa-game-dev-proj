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
    public int AcceptedContractIndex { get; private set; }
    public UnityEvent OnContractChanged = new UnityEvent();

    [field: SerializeField] public double DifficultyCap { get; private set; }
    [field: SerializeField] public double DifficultyCapIncrease { get; private set; }

    private AudioSource audioSource;
    [SerializeField] private AudioClip contractCompleteSfx;

    protected override void OnStart()
    {
        Inventory = new Inventory(1, true, Player.PlayerInventory);
        GenerateContracts();
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

    private Contract CreateRandomContract(int attempts)
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

            contract.MinPower = baseLevelStats.Power * level * randMult;
            difficulty += (level - 2) * randMult;
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

        if (contract.Difficulty > DifficultyCap)
        {
            Contract alternative = CreateRandomContract(attempts - 1);
            if (alternative.Difficulty < contract.Difficulty)
                contract = alternative;
        }
        return contract;
    }

    private void ReplaceContractAtIndex(int index)
    {
        AvailableContracts.RemoveAt(index);
        //note: i decided not to bother checking for duplicates here purely because its so unlikely
        AvailableContracts.Insert(index, CreateRandomContract(100));
    }

    public void GenerateContracts()
    {
        AvailableContracts.Clear();
        for (int i = 0; i < 3; i++)
            AvailableContracts.Add(CreateRandomContract(100));
        OnContractChanged.Invoke();
    }

    public void AcceptContract(int contractIndex)
    {
        if (AcceptedContract != null) return;
        //if (!AvailableContracts.Contains(contract)) return;

        AcceptedContractIndex = contractIndex;
        AcceptedContract = AvailableContracts[contractIndex];

        //AvailableContracts.Clear();

        OnContractChanged.Invoke();
    }

    public void CancelContract()
    {
        AcceptedContract = null;
        OnContractChanged.Invoke();
    }

    public void TryFulfillContract()
    {
        if (AcceptedContract == null) return;
        if (!Inventory.SlotContainsItem(0)) return;

        IItem item = Inventory.ItemInSlot(0);
        Debug.Log(AcceptedContract.IsSatisfied(item));
        if (AcceptedContract.IsSatisfied(item))
        {
            FulfillContract();
        }
    }

    //im sorry for doing this nikhil
    //this shouldnt be called anywhere other than the tutorial
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

        DifficultyCap += DifficultyCapIncrease;

        ReplaceContractAtIndex(AcceptedContractIndex);
        AcceptedContract = null;

        //GenerateContracts();
        audioSource.clip = contractCompleteSfx;
        audioSource.Play();
        OnContractChanged.Invoke();
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

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
