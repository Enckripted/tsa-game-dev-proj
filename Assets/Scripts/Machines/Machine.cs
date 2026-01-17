using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Recipe
{
    public double Duration { get; }
    public IEnumerable<FragmentQuantity> ComponentInputs { get; }
    public IEnumerable<FragmentQuantity> ComponentOutputs { get; }
    public IEnumerable<IItem> ItemOutputs { get; }
    public Recipe(double nDuration, IEnumerable<FragmentQuantity> nCompInputs, IEnumerable<FragmentQuantity> nCompOutputs, IEnumerable<IItem> nItemOutputs) //ienumerable is a read only list, which is what we want in this case
    {
        Duration = nDuration;
        ComponentInputs = nCompInputs;
        ComponentOutputs = nCompOutputs;
        ItemOutputs = nItemOutputs;
    }
}

public interface IMachine
{
    public Inventory InputSlots { get; }
    public Inventory OutputSlots { get; }
    public bool RunsAutomatically { get; }

    public Recipe? CurrentRecipe { get; }
    public double SecondsRemaining { get; }
    public bool Running { get; }

    public bool HasValidRecipe();
    public void AttemptMachineStart();
    public void AttemptMachineStop();
}

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(AudioSource))]
public abstract class BaseMachine : TileEntity, IMachine
{
    public abstract int NumInputSlots { get; }
    public abstract int NumOutputSlots { get; }
    public abstract bool RunsAutomatically { get; }
    public abstract bool StopsWhenFinished { get; }

    public Inventory InputSlots { get; private set; }
    public Inventory OutputSlots { get; private set; }

    public Recipe? CurrentRecipe { get; protected set; }
    [field: SerializeField] public double SecondsRemaining { get; protected set; }
    [field: SerializeField] public bool Running { get; protected set; }

    public abstract bool HasValidRecipe();
    protected abstract Recipe GetRecipe();
    protected abstract void ExtractItemInputs();
    protected abstract void OnRecipeEnd();
    protected abstract void MachineUpdate();
    protected abstract void LoadMachineIntoUi(GameObject uiInstance);

    protected AudioSource MachineAudioSource;

    private void UpdateRecipe()
    {
        if (Running) return;
        if (HasValidRecipe()) CurrentRecipe = GetRecipe();
        else CurrentRecipe = null;
    }

    private bool CanRunRecipe()
    {
        return CurrentRecipe.HasValue &&
        OutputSlots.AvailableSlots >= CurrentRecipe.Value.ItemOutputs.Count<IItem>() &&
        Player.PlayerComponents.HasQuantitiesAvailable(CurrentRecipe.Value.ComponentInputs);
    }

    protected void StartRecipe()
    {
        SecondsRemaining = CurrentRecipe.Value.Duration;
        Running = true;

        foreach (FragmentQuantity compQuant in CurrentRecipe.Value.ComponentInputs)
            Player.PlayerComponents.SubtractComponentQuantity(compQuant);
        ExtractItemInputs();
    }

    protected void EndRecipe()
    {
        Running = false;

        foreach (FragmentQuantity compQuant in CurrentRecipe.Value.ComponentOutputs)
            Player.PlayerComponents.AddComponentQuantity(compQuant);
        foreach (IItem output in CurrentRecipe.Value.ItemOutputs)
            OutputSlots.PushItem(output);

        OnRecipeEnd();
        UpdateRecipe();
        if (CurrentRecipe != null && !StopsWhenFinished) StartRecipe();
    }

    public void AttemptMachineStart()
    {
        if (!Running && CurrentRecipe.HasValue && CanRunRecipe()) StartRecipe();
    }

    public void AttemptMachineStop()
    {
        if (!Running) return;
        Running = false;
        UpdateRecipe();
    }

    public override void LoadUi(GameObject uiInstance)
    {
        Debug.Log(InputSlots);
        LoadMachineIntoUi(uiInstance);
        Player.PlayerInventory.TargetInventory = InputSlots;
    }

    public override void UnloadUi(GameObject uiInstance)
    {
        Player.PlayerInventory.TargetInventory = null;
    }

    protected override void OnStart()
    {
        InputSlots = new Inventory(NumInputSlots, Player.PlayerInventory);
        OutputSlots = new Inventory(NumOutputSlots, Player.PlayerInventory);
        MachineAudioSource = GetComponent<AudioSource>();
        InputSlots.Changed.AddListener(UpdateRecipe);
    }

    void Update()
    {
        SecondsRemaining -= Time.deltaTime;
        if (Running && SecondsRemaining <= 0) EndRecipe();
        if (!Running && RunsAutomatically) AttemptMachineStart();
        MachineUpdate();
    }
}
