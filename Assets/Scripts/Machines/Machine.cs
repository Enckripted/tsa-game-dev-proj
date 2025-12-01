using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Recipe
{
    public double duration { get; }
    public IEnumerable<ComponentQuantity> componentInputs { get; }
    public IEnumerable<ComponentQuantity> componentOutputs { get; }
    public IEnumerable<Item> itemOutputs { get; }
    public Recipe(double nDuration, IEnumerable<ComponentQuantity> nCompInputs, IEnumerable<ComponentQuantity> nCompOutputs, IEnumerable<Item> nItemOutputs) //ienumerable is a read only list, which is what we want in this case
    {
        duration = nDuration;
        componentInputs = nCompInputs;
        componentOutputs = nCompOutputs;
        itemOutputs = nItemOutputs;
    }
}

public interface IMachine
{
    public Inventory inputSlots { get; }
    public Inventory outputSlots { get; }
    public bool runsAutomatically { get; }
    public GameObject uiPrefab { get; }

    public Recipe? currentRecipe { get; }
    public double secondsRemaining { get; }
    public bool running { get; }

    public bool hasValidRecipe();
    public void attemptMachineStart();
    public void attemptMachineStop();
}

[RequireComponent(typeof(Interactable))]
public abstract class BaseMachine : MonoBehaviour, IMachine
{
    public abstract int numInputSlots { get; }
    public abstract int numOutputSlots { get; }
    public abstract bool runsAutomatically { get; }
    public abstract bool stopsWhenFinished { get; }
    public abstract GameObject uiPrefab { get; protected set; }

    public Inventory inputSlots { get; private set; }
    public Inventory outputSlots { get; private set; }

    public Recipe? currentRecipe { get; protected set; }
    [field: SerializeField] public double secondsRemaining { get; protected set; }
    [field: SerializeField] public bool running { get; protected set; }

    private Interactable interactable;

    abstract public bool hasValidRecipe();
    abstract protected Recipe getRecipe();
    abstract protected void extractItemInputs();

    private void updateRecipe()
    {
        if (running) return;
        if (hasValidRecipe()) currentRecipe = getRecipe();
        else currentRecipe = null;
    }

    private bool canRunRecipe()
    {
        return currentRecipe.HasValue &&
        outputSlots.availableSlots >= currentRecipe.Value.itemOutputs.Count<Item>() &&
        ComponentInventory.instance.hasQuantitiesAvailable(currentRecipe.Value.componentInputs);
    }

    protected void startRecipe()
    {
        secondsRemaining = currentRecipe.Value.duration;
        running = true;

        foreach (ComponentQuantity compQuant in currentRecipe.Value.componentInputs)
            ComponentInventory.instance.subtractComponentQuantity(compQuant);
        extractItemInputs();
    }

    protected void endRecipe()
    {
        running = false;

        foreach (ComponentQuantity compQuant in currentRecipe.Value.componentOutputs)
            ComponentInventory.instance.addComponentQuantity(compQuant);
        foreach (Item output in currentRecipe.Value.itemOutputs)
            outputSlots.pushItem(output);

        updateRecipe();
        if (currentRecipe != null && !stopsWhenFinished) startRecipe();
    }

    public void attemptMachineStart()
    {
        if (!running && currentRecipe.HasValue && canRunRecipe()) startRecipe();
    }

    public void attemptMachineStop()
    {
        if (!running) return;
        running = false;
        updateRecipe();
    }

    public void openMachineUi()
    {
        MachineUiManager.instance.openUi(this, uiPrefab);
        //if (!ReferenceEquals(MachineUiManager.instance.currentMachine, this)) MachineUiManager.instance.loadMachine(this);
    }

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();
        if (!running && runsAutomatically) attemptMachineStart();
    }

    void Awake()
    {
        inputSlots = new Inventory(numInputSlots);
        outputSlots = new Inventory(numOutputSlots);
        inputSlots.changed.AddListener(updateRecipe);
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.interactEvent.AddListener(openMachineUi);
    }
}
