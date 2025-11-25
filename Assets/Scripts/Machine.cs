using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal.Internal;

public struct Recipe
{
    public double duration { get; }
    public IEnumerable<ComponentQuantity> componentOutputs { get; }
    public IEnumerable<Item> itemOutputs { get; }
    public Recipe(double nDuration, IEnumerable<ComponentQuantity> nCompOutputs, IEnumerable<Item> nItemOutputs) //ienumerable is a read only list, which is what we want in this case
    {
        duration = nDuration;
        componentOutputs = nCompOutputs;
        itemOutputs = nItemOutputs;
    }
}

public interface Machine
{
    public Inventory inputSlots { get; }
    public Inventory outputSlots { get; }
    public bool runsAutomatically { get; }

    public Recipe currentRecipe { get; }
    public double secondsRemaining { get; }
    public bool running { get; }

    public bool canRunRecipe();
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

    public Recipe currentRecipe { get; protected set; }
    public double secondsRemaining { get; protected set; }
    public bool running { get; protected set; }

    abstract public bool canRunRecipe();
    abstract protected Recipe getRecipe();
    abstract protected void extractRecipeInputs();

    protected void runRecipe(Recipe recipe)
    {
        secondsRemaining = recipe.duration;
        currentRecipe = recipe;
        running = true;
    }

    protected void endRecipe()
    {
        foreach (ComponentQuantity compQuant in currentRecipe.componentOutputs)
        {
            ComponentInventory.instance.addComponentQuantity(compQuant);
        }

        foreach (Item output in currentRecipe.itemOutputs)
        {
            outputSlots.pushItem(output);
        }

        running = false;
    }

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();

        if (running || !canRunRecipe()) return;

        Recipe recipe = getRecipe();
        if (outputSlots.availableSlots < recipe.itemOutputs.Count<Item>())
            return;
        extractRecipeInputs();
        runRecipe(recipe);
        Debug.Log("running recipe");
    }

    void Awake()
    {
        //temporary solution instead of letting the child class define how many inventory slots they like
        inputSlots = new Inventory(numInventorySlots);
        outputSlots = new Inventory(numInventorySlots);
    }

    //temporary code since i dont have an interaction system yet
    public void OnPointerClick(PointerEventData eventData)
    {
        MachineUiManager.instance.loadMachine(this);
    }
}
