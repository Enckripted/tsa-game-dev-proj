using System.Collections.Generic;
using UnityEngine;

public class FragmentInventoryUi : MonoBehaviour
{
    public FragmentInventory Inventory;
    [SerializeField] private GameObject fragmentQuantityElement;

    void Start()
    {
        foreach (FragmentQuantity quantity in Inventory)
        {
            GameObject nQuantity = Instantiate(fragmentQuantityElement, transform);
            nQuantity.GetComponent<FragmentQuantityUi>().Quantity = quantity;
        }
    }
}
