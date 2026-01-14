using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Recipe
{
    public double duration { get; }
    public IEnumerable<ComponentQuantity> componentInputs { get; }
    public IEnumerable<ComponentQuantity> componentOutputs { get; }
    public IEnumerable<IItem> itemOutputs { get; }
    public Recipe(double nDuration, IEnumerable<ComponentQuantity> nCompInputs, IEnumerable<ComponentQuantity> nCompOutputs, IEnumerable<IItem> nItemOutputs) //ienumerable is a read only list, which is what we want in this case
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

    public Recipe? currentRecipe { get; }
    public double secondsRemaining { get; }
    public bool running { get; }

    public bool hasValidRecipe();
    public void attemptMachineStart();
    public void attemptMachineStop();
}

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(AudioSource))]
public abstract class BaseMachine : TileEntity, IMachine
{
    public abstract int numInputSlots { get; }
    public abstract int numOutputSlots { get; }
    public abstract bool runsAutomatically { get; }
    public abstract bool stopsWhenFinished { get; }

    public Inventory inputSlots { get; private set; }
    public Inventory outputSlots { get; private set; }

    public Recipe? currentRecipe { get; protected set; }
    [field: SerializeField] public double secondsRemaining { get; protected set; }
    [field: SerializeField] public bool running { get; protected set; }

    public abstract bool hasValidRecipe();
    protected abstract Recipe getRecipe();
    protected abstract void extractItemInputs();
    protected abstract void onRecipeEnd();
    protected abstract void machineUpdate();
    protected abstract void loadMachineIntoUi(GameObject uiInstance);

    protected AudioSource audioSource;

    private void updateRecipe()
    {
        if (running) return;
        if (hasValidRecipe()) currentRecipe = getRecipe();
        else currentRecipe = null;
    }

    private bool canRunRecipe()
    {
        return currentRecipe.HasValue &&
        outputSlots.AvailableSlots >= currentRecipe.Value.itemOutputs.Count<IItem>() &&
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
        foreach (IItem output in currentRecipe.Value.itemOutputs)
            outputSlots.PushItem(output);

        onRecipeEnd();
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

    public override void loadUi(GameObject uiInstance)
    {
        loadMachineIntoUi(uiInstance);
        Player.PlayerInventory.TargetInventory = inputSlots;
    }

    public override void unloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }

    protected override void onStart()
    {
        inputSlots = new Inventory(numInputSlots, Player.PlayerInventory);
        outputSlots = new Inventory(numOutputSlots, Player.PlayerInventory);
        audioSource = GetComponent<AudioSource>();
        inputSlots.Changed.AddListener(updateRecipe);
    }

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();
        if (!running && runsAutomatically) attemptMachineStart();
        machineUpdate();
    }
}
