using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockUpgrade
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public List<UnlockUpgradeTier> Tiers { get; private set; }

    public int CurrentTier { get; private set; } = 0;

    public UnlockUpgradeTier GetCurrentTier()
    {
        return Tiers[CurrentTier];
    }

    public void BuyUpgrade()
    {
        if (CurrentTier == Tiers.Count || !Player.HasMoney(GetCurrentTier().Cost)) return;

        //TODO: probably add some sort of visual effect to this
        Player.RemoveMoney(GetCurrentTier().Cost);
        GetCurrentTier().ObjectUnlock.SetActive(true);
        CurrentTier++;
    }
}
