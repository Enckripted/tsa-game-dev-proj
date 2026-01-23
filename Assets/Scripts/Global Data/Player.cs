using UnityEngine;

public class Player : MonoBehaviour
{
    public static double Money;
    public static Inventory PlayerInventory;
    public static FragmentInventory PlayerFragments;

    void Awake()
    {
        Money = 0;
        PlayerInventory = new Inventory(8);

        PlayerFragments = new FragmentInventory();
        foreach (MaterialScriptableObject material in ScriptableObjectData.BaseMaterials)
        {
            PlayerFragments.AddFragmentQuantity(new FragmentQuantity(material.name, 0));
        }
    }

    public static bool HasMoney(double amount) => Money >= amount;
    public static void AddMoney(double amount) => Money += amount;
    public static void RemoveMoney(double amount)
    {
        if (!HasMoney(amount)) throw new System.Exception("Not enough money to subtract " + amount);
        Money -= amount;
    }
}
