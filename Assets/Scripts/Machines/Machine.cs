using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Allows machines to be passed in generically in other places.
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

//This abstract class handles the logic for starting and ending recipes, loading and unloading the
//UI, and provides abstract functions that member classes will override to implement their own
//behavior.
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
        Player.PlayerFragments.Contains(CurrentRecipe.Value.FragmentInputs);
    }

    protected void StartRecipe()
    {
        SecondsRemaining = CurrentRecipe.Value.Duration;
        Running = true;

        foreach (FragmentQuantity fragmentQuantity in CurrentRecipe.Value.FragmentInputs)
            Player.PlayerFragments.SubFragmentQuantity(fragmentQuantity);
        ExtractItemInputs();
    }

    protected void EndRecipe()
    {
        Running = false;

        foreach (FragmentQuantity fragmentQuantity in CurrentRecipe.Value.FragmentOutputs)
            Player.PlayerFragments.AddFragmentQuantity(fragmentQuantity);
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
        InputSlots = new Inventory(NumInputSlots, true, Player.PlayerInventory);
        OutputSlots = new Inventory(NumOutputSlots, false, Player.PlayerInventory);
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
