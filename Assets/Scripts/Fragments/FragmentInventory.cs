using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: maybe add safety checks for strings that aren't in our material database?

//Wrapper for dictionary containing Fragments
public class FragmentInventory : IEnumerable
{
    //as far as i know there's no real downside to using serializeddictionary instead of a normal dictionary
    //so im just gonna keep this here
    [field: SerializeField] public List<FragmentQuantity> Fragments { get; private set; }

    public FragmentInventory() : this(new List<FragmentQuantity>()) { }

    public FragmentInventory(List<FragmentQuantity> startingQuantity)
    {
        Fragments = startingQuantity;
    }

    public IEnumerator GetEnumerator()
    {
        return Fragments.GetEnumerator();
    }

    private bool KeyExists(FragmentQuantity target)
    {
        foreach (FragmentQuantity quantity in Fragments) if (quantity.Type == target.Type) return true;
        return false;
    }

    //assumes the key exists
    private FragmentQuantity GetMatchingQuantity(FragmentQuantity target)
    {
        foreach (FragmentQuantity quantity in Fragments) if (quantity.Type == target.Type) return quantity;
        throw new Exception("Wasn't able to find quantity of type " + target.Type);
    }

    public bool Contains(FragmentQuantity quantity)
    {
        return KeyExists(quantity) && GetMatchingQuantity(quantity).Amount >= quantity.Amount;
    }

    public bool Contains(FragmentInventory quantities)
    {
        foreach (FragmentQuantity quantity in quantities) if (!KeyExists(quantity) || !Contains(quantity)) return false;
        return true;
    }

    public void AddFragmentQuantity(FragmentQuantity quantity)
    {
        if (!KeyExists(quantity)) Fragments.Add(quantity);
        GetMatchingQuantity(quantity).Amount += quantity.Amount;
    }

    public void SubFragmentQuantity(FragmentQuantity quantity)
    {
        if (!KeyExists(quantity) || GetMatchingQuantity(quantity).Amount < quantity.Amount) throw new Exception("Attempted to make negative " + quantity.Type);
        GetMatchingQuantity(quantity).Amount -= quantity.Amount;
    }

    public void AddFragments(FragmentInventory inventory)
    {
        foreach (FragmentQuantity quantity in inventory) AddFragmentQuantity(quantity);
    }

    //assumes a check has been done with .Contains
    public void SubFragments(FragmentInventory inventory)
    {
        foreach (FragmentQuantity quantity in inventory) SubFragmentQuantity(quantity);
    }
}
