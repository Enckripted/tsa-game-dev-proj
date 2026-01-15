using System;
using UnityEngine;

[Serializable]
public class ComponentQuantity
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public uint Amount { get; private set; } //must always be positive

    public ComponentQuantity(string matType, uint matAmount)
    {
        Type = matType;
        Amount = matAmount;
    }
}
