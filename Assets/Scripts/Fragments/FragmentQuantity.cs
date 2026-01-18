using System;
using UnityEngine;

//Simple representation of fragment amounts
[Serializable]
public class FragmentQuantity
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public uint Amount { get; set; } //must always be positive

    public FragmentQuantity(string matType, uint matAmount)
    {
        Type = matType;
        Amount = matAmount;
    }

    public FragmentQuantity(Material material, uint amount)
    {
        Type = material.Name;
        Amount = amount;
    }
}
