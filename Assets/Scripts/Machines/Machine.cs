using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

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

public interface Machine
{
    public Inventory inputSlots { get; }
    public Inventory outputSlots { get; }
    public bool runsAutomatically { get; }

    public Recipe? currentRecipe { get; }
    public double secondsRemaining { get; }
    public bool running { get; }

    public bool hasValidRecipe();
    public void attemptMachineStart();
}

public abstract class BaseMachine : MonoBehaviour, Machine, IPointerClickHandler
{
    private const int numInventorySlots = 12;

    public Inventory inputSlots { get; private set; }
    public Inventory outputSlots { get; private set; }
    //i've learned that there's only a couple ways to override a field from an abstract class
    //what you're expected to do here is override the getter in your new machine class and return true or false based on
    //whether the machine automatically runs
    public abstract bool runsAutomatically { get; }
    public Recipe? currentRecipe { get; protected set; } //make this a field later
    [field: SerializeField] public double secondsRemaining { get; protected set; }
    [field: SerializeField] public bool running { get; protected set; }

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
    }

    public void attemptMachineStart()
    {
        if (!running && currentRecipe.HasValue && canRunRecipe()) startRecipe();
    }

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();
        if (!running && runsAutomatically) attemptMachineStart();
    }

    void Awake()
    {
        //temporary solution instead of letting the child class define how many inventory slots they like
        inputSlots = new Inventory(numInventorySlots);
        outputSlots = new Inventory(numInventorySlots);
        inputSlots.changed.AddListener(updateRecipe);
    }

    //temporary code since i dont have an interaction system yet
    public void OnPointerClick(PointerEventData eventData)
    {
        MachineUiManager.instance.loadMachine(this);
    }
}
