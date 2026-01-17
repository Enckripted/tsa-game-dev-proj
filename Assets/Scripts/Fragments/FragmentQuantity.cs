using System;
using UnityEngine;

[Serializable]
public class FragmentQuantity
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public uint Amount { get; private set; } //must always be positive

    public FragmentQuantity(string matType, uint matAmount)
    {
        Type = matType;
        Amount = matAmount;
    }
}
