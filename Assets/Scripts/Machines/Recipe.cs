//A basic container, where machines will put the fragments they will consume, the fragments and
//items they will output along with the recipe duration. Prevents repetition across other machines'
//code.
using System.Collections.Generic;

public struct Recipe
{
    public double Duration { get; }
    public FragmentInventory FragmentInputs { get; }
    public FragmentInventory FragmentOutputs { get; }
    public IEnumerable<IItem> ItemOutputs { get; }
    public Recipe(double nDuration, FragmentInventory nFragInputs, FragmentInventory nFragOutputs, IEnumerable<IItem> nItemOutputs) //ienumerable is a read only list, which is what we want in this case
    {
        Duration = nDuration;
        FragmentInputs = nFragInputs;
        FragmentOutputs = nFragOutputs;
        ItemOutputs = nItemOutputs;
    }
}
