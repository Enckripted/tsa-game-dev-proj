using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnvilMachine : BaseMachine
{
    public override int numInputSlots => 1;
    public override int numOutputSlots => 1;
    public override bool runsAutomatically => false;
    public override bool stopsWhenFinished => true;

    [field: SerializeField] public override GameObject uiPrefab { get; protected set; }
    [SerializeField] private AudioClip runningSfx;
    [SerializeField] private float runningSfxDelaySecs;
    [SerializeField] private AudioClip finishSfx;

    public override bool hasValidRecipe()
    {
        return inputSlots.AvailableSlots == 0;
    }

    protected override Recipe getRecipe()
    {
        WandItem reference = (inputSlots.ItemInSlot(0) as WandItem);
        WandReforgeScriptableObject reforgeData = ScriptableObjectData.RandomWandReforgeData();
        WandItem output = new WandItem(reference.BaseName, reference.Level, reference.BaseStats, reference.LevelStats, reference.WandMaterial, new WandReforge(reforgeData));

        IEnumerable<ComponentQuantity> componentInputs = new List<ComponentQuantity> { new ComponentQuantity(reference.WandMaterial.Name, 10) };
        IEnumerable<ComponentQuantity> componentOutputs = new List<ComponentQuantity> { };
        IEnumerable<IItem> itemOutputs = new List<WandItem> { output };
        return new Recipe(15.0, componentInputs, componentOutputs, itemOutputs);
    }

    protected override void extractItemInputs()
    {
        inputSlots.GetSlot(0).Pop();
        audioSource.clip = runningSfx;
        audioSource.Play();
    }

    protected override void onRecipeEnd()
    {
    }

    protected override void machineUpdate()
    {
        if (running && !audioSource.isPlaying)
        {
            audioSource.PlayDelayed(runningSfxDelaySecs);
        }
    }

    protected override void loadMachineIntoUi(GameObject uiInstance)
    {
        AnvilMachineUi ui = uiInstance.GetComponent<AnvilMachineUi>();
        ui.machine = this;
    }
}
