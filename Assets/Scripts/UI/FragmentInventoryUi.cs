using System.Collections.Generic;
using UnityEngine;

public class FragmentInventoryUi : MonoBehaviour
{
    [SerializeField] private GameObject componentQuantityElement;

    void Start()
    {
        foreach (KeyValuePair<string, uint> pair in Player.PlayerComponents.Components)
        {
            GameObject nQuantity = Instantiate(componentQuantityElement, transform);
            nQuantity.GetComponent<FragmentQuantityUi>().ComponentType = pair.Key;
        }
    }
}
