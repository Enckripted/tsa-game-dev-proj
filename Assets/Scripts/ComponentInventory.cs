using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//TODO: maybe add safety checks for strings that aren't in our material database?
public class ComponentInventory
{
    //as far as i know there's no real downside to using serializeddictionary instead of a normal dictionary
    //so im just gonna keep this here
    [field: SerializeField] public SerializedDictionary<string, uint> Components { get; private set; }

    public ComponentInventory()
    {
        Components = new SerializedDictionary<string, uint>();
    }

    private bool MatTypeInInventory(string matType)
    {
        return Components.ContainsKey(matType);
    }

    public bool HasQuantityAvailable(ComponentQuantity compQuant)
    {
        return Components[compQuant.Type] >= compQuant.Amount;
    }

    public bool HasQuantitiesAvailable(IEnumerable<ComponentQuantity> compQuants)
    {
        foreach (ComponentQuantity compQuant in compQuants)
        {
            if (!HasQuantityAvailable(compQuant)) return false;
        }
        return true;
    }

    public uint GetQuantity(string matType)
    {
        if (!MatTypeInInventory(matType)) Components[matType] = 0;
        return Components[matType];
    }

    public void AddComponentQuantity(ComponentQuantity compQuant)
    {
        if (!MatTypeInInventory(compQuant.Type)) Components[compQuant.Type] = 0;
        Components[compQuant.Type] += compQuant.Amount;
    }

    public void SubtractComponentQuantity(ComponentQuantity compQuant)
    {
        if (!MatTypeInInventory(compQuant.Type)) Components[compQuant.Type] = 0;
        if (GetQuantity(compQuant.Type) - compQuant.Amount < 0) throw new Exception("Subtracted more than current amount for component type called " + compQuant.Type);
        Components[compQuant.Type] -= compQuant.Amount;
    }
}
