using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnvilMachine : BaseMachine
{
    public override int NumInputSlots => 1;
    public override int NumOutputSlots => 1;
    public override bool RunsAutomatically => false;
    public override bool StopsWhenFinished => true;

    [field: SerializeField] public override GameObject UiPrefab { get; protected set; }
    [SerializeField] private AudioClip runningSfx;
    [SerializeField] private float runningSfxDelaySecs;
    [SerializeField] private AudioClip finishSfx;

    //assumes there is an item inside of the anvil
    private FragmentInventory GetFragmentCost()
    {
        WandItem reference = InputSlots.ItemInSlot(0) as WandItem;
        return new FragmentInventory(new List<FragmentQuantity>()
        {
            new FragmentQuantity(reference.WandMaterial, 10)
        });
    }

    public override bool HasValidRecipe()
    {
        return InputSlots.AvailableSlots == 0 && Player.PlayerFragments.Contains(GetFragmentCost());
    }

    protected override Recipe GetRecipe()
    {
        WandItem reference = (InputSlots.ItemInSlot(0) as WandItem);
        WandReforgeScriptableObject reforgeData = ScriptableObjectData.RandomWandReforgeData();
        WandItem output = new WandItem(reference.BaseName, reference.Level, reference.BaseStats, reference.LevelStats, reference.WandMaterial, reference.GemSlots, reference.Gems, new WandReforge(reforgeData));

        IEnumerable<IItem> itemOutputs = new List<WandItem> { output };
        return new Recipe(GameState.TutorialRunning ? 3.0 : 15.0, GetFragmentCost(), new FragmentInventory(), itemOutputs);
    }

    protected override void ExtractItemInputs()
    {
        InputSlots.GetSlot(0).Pop();
        MachineAudioSource.clip = runningSfx;
        MachineAudioSource.Play();
    }

    protected override void OnRecipeEnd()
    {
        Player.ItemsReforged++;
    }

    protected override void MachineUpdate()
    {
        if (Running && !MachineAudioSource.isPlaying)
        {
            MachineAudioSource.PlayDelayed(runningSfxDelaySecs);
        }
    }

    protected override void LoadMachineIntoUi(GameObject uiInstance)
    {
        AnvilMachineUi ui = uiInstance.GetComponent<AnvilMachineUi>();
        ui.Machine = this;
    }
}
