using System.Collections.Generic;
using UnityEngine;

public struct Recipe
{
    public double duration { get; }
    public List<Item> outputs { get; }
    public Recipe(double nDuration, List<Item> nOutputs)
    {
        duration = nDuration;
        outputs = nOutputs;
    }
}

public abstract class Machine : MonoBehaviour
{
    public Inventory inputSlots { get; }
    public Inventory outputSlots { get; }

    //list for now because idk whether we'll have multi output recipes
    public List<Item> recipeOutputs { get; protected set; }
    public double secondsRemaining { get; protected set; }
    public bool running { get; protected set; }

    protected abstract bool canRunRecipe();
    protected abstract Recipe getRecipe();
    protected abstract void extractRecipeInputs();

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

    void Update()
    {
        secondsRemaining -= Time.deltaTime;
        if (running && secondsRemaining <= 0) endRecipe();

        if (!canRunRecipe()) return;

        Recipe recipe = getRecipe();
        if (outputSlots.availableSlots < recipe.outputs.Count)
            return;
        runRecipe(recipe);
    }
}
