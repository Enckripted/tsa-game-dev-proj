using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorial : MonoBehaviour
{
    [SerializeField] private StorageBox itemSpawnBox;
    [SerializeField] private ContractTileEntity contractTileEntity;
    [SerializeField] private LevellerMachine firstLevellerMachine;
    [SerializeField] private bool tutorialWhileInDevMode;

    [SerializeField] private List<string> startTutorial;
    [SerializeField] private List<string> processItemsTutorial;
    [SerializeField] private List<string> enhanceItemsTutorial;
    [SerializeField] private List<string> acceptContractsTutorial;
    [SerializeField] private List<string> upgradesTutorial;
    [SerializeField] private List<string> closingTutorial;

    private WandScriptableObject baseWandData;
    private MaterialScriptableObject materialData;

    private void SpawnWandInBox()
    {
        itemSpawnBox.StorageSlots.PushItem(new WandItem(
            baseWandData, 1, new Material(materialData)
        ));
    }

    private List<Contract> GeneratePersonalizedContracts()
    {
        List<Contract> result = new List<Contract>();
        for (int i = 0; i < 3; i++)
        {
            Contract contract = new Contract();
            contract.RequiredBaseName = baseWandData.Name;
            contract.RequiredMaterial = new Material(materialData);
            contract.MinPower = 1;
            contract.MinSellValue = 0.1;
            contract.Reward = 2;
            result.Add(contract);
        }
        return result;
    }

    IEnumerator ClosingTutorial()
    {
        while (!firstLevellerMachine.isActiveAndEnabled) yield return null;
        TutorialManagerUi.DoTutorialMessages(closingTutorial, () =>
        {
            GameState.TutorialRunning = false;
        });
    }

    IEnumerator UpgradesTutorial()
    {
        while (Player.ContractsCompleted < 1) yield return null;
        TutorialManagerUi.DoTutorialMessages(upgradesTutorial, () =>
        {
            StartCoroutine(ClosingTutorial());
        });
    }

    IEnumerator AcceptContractsTutorial()
    {
        while (Player.ItemsReforged < 1) yield return null;
        TutorialManagerUi.DoTutorialMessages(acceptContractsTutorial, () =>
        {
            StartCoroutine(UpgradesTutorial());
        });
    }

    IEnumerator EnhanceItemsTutorial()
    {
        while (Player.ItemsMelted < 2) yield return null;
        SpawnWandInBox(); //FIXME: you can softlock the tutorial by simply melting this new wand you're given
        TutorialManagerUi.DoTutorialMessages(enhanceItemsTutorial, () =>
        {
            StartCoroutine(AcceptContractsTutorial());
        });
    }

    IEnumerator ProcessItemsTutorial()
    {
        while (itemSpawnBox.StorageSlots.UsedSlots > 0) yield return null; //no advancing until they pick up the two items we'll spawn in the box
        TutorialManagerUi.DoTutorialMessages(processItemsTutorial, () =>
        {
            StartCoroutine(EnhanceItemsTutorial());
        });
    }

    public void BeginTutorial()
    {
        GameState.TutorialRunning = true;

        TutorialManagerUi.DoTutorialMessages(startTutorial, () =>
        {
            //we need to give some time for everything to initialize
            //once again this is horrible design done in a time crunch
            contractTileEntity.OverrideContracts(GeneratePersonalizedContracts());
            for (int i = 0; i < 2; i++) SpawnWandInBox();

            StartCoroutine(ProcessItemsTutorial());
        });
    }

    void Start()
    {
        baseWandData = ScriptableObjectData.BaseWands[0];
        materialData = ScriptableObjectData.BaseMaterials[0];
        if (!Debug.isDebugBuild || tutorialWhileInDevMode) BeginTutorial();
    }
}
