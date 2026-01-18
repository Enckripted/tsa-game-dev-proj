using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//TODO: maybe add safety checks for strings that aren't in our material database?

//Wrapper for dictionary containing Fragments
public class FragmentInventory
{
    //as far as i know there's no real downside to using serializeddictionary instead of a normal dictionary
    //so im just gonna keep this here
    [field: SerializeField] public SerializedDictionary<string, uint> Components { get; private set; }

    public FragmentInventory()
    {
        Components = new SerializedDictionary<string, uint>();
    }

    private bool MatTypeInInventory(string matType)
    {
        return Components.ContainsKey(matType);
    }

    public bool HasQuantityAvailable(FragmentQuantity fragmentQuantity)
    {
        return Components[fragmentQuantity.Type] >= fragmentQuantity.Amount;
    }

    public bool HasQuantitiesAvailable(IEnumerable<FragmentQuantity> fragmentQuantitys)
    {
        foreach (FragmentQuantity fragmentQuantity in fragmentQuantitys)
        {
            if (!HasQuantityAvailable(fragmentQuantity)) return false;
        }
        return true;
    }

    public uint GetQuantity(string matType)
    {
        if (!MatTypeInInventory(matType)) Components[matType] = 0;
        return Components[matType];
    }

    public void AddComponentQuantity(FragmentQuantity fragmentQuantity)
    {
        if (!MatTypeInInventory(fragmentQuantity.Type)) Components[fragmentQuantity.Type] = 0;
        Components[fragmentQuantity.Type] += fragmentQuantity.Amount;
    }

    public void SubtractComponentQuantity(FragmentQuantity fragmentQuantity)
    {
        if (!MatTypeInInventory(fragmentQuantity.Type)) Components[fragmentQuantity.Type] = 0;
        if (GetQuantity(fragmentQuantity.Type) - fragmentQuantity.Amount < 0) throw new Exception("Subtracted more than current amount for fragment type called " + fragmentQuantity.Type);
        Components[fragmentQuantity.Type] -= fragmentQuantity.Amount;
    }
}
