using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal.Internal;

public struct Recipe
{
    public double duration { get; }
    public IEnumerable<Item> outputs { get; }
    public Recipe(double nDuration, IEnumerable<Item> nOutputs) //ienumerable is a read only list, which is what we want in this case
    {
        duration = nDuration;
        outputs = nOutputs;
    }
}

public interface Machine
{
    public Inventory inputSlots { get; }
    public Inventory outputSlots { get; }

    public IEnumerable<Item> recipeOutputs { get; }
    public double secondsRemaining { get; }
    public bool running { get; }

    public bool canRunRecipe();
}

public abstract class BaseMachine : MonoBehaviour, Machine, IPointerClickHandler
{
    private const int numInventorySlots = 12;

    public Inventory inputSlots { get; private set; }
    public Inventory outputSlots { get; private set; }

    public IEnumerable<Item> recipeOutputs { get; protected set; }
    public double secondsRemaining { get; protected set; }
    public bool running { get; protected set; }

    abstract public bool canRunRecipe();
    abstract protected Recipe getRecipe();
    abstract protected void extractRecipeInputs();

    protected void runRecipe(Recipe recipe)
    {
        secondsRemaining = recipe.duration;
        recipeOutputs = recipe.outputs;
        running = true;
    }

    protected void endRecipe()
    {
        foreach (Item output in recipeOutputs)
        {
            outputSlots.pushItem(output);
        }

        running = false;
    }

    void Awake()
    {
        //temporary solution instead of letting the child class define how many inventory slots they like
        inputSlots = new Inventory(numInventorySlots);
        outputSlots = new Inventory(numInventorySlots);
    }

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();

        if (running || !canRunRecipe()) return;

        Recipe recipe = getRecipe();
        if (outputSlots.availableSlots < recipe.outputs.Count<Item>())
            return;
        extractRecipeInputs();
        runRecipe(recipe);
        Debug.Log("running recipe");
    }

    //temporary code since i dont have an interaction system yet
    public void OnPointerClick(PointerEventData eventData)
    {
        MachineUiManager.instance.loadMachine(this);
    }
}
