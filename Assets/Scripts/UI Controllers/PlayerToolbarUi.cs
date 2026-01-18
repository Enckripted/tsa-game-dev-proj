using TMPro;
using UnityEngine;

public class PlayerToolbarUi : MonoBehaviour
{
    [field: SerializeField] public InventoryUi PlayerInventoryUi { get; private set; }
    [field: SerializeField] public FragmentInventoryUi PlayerFragmentUi { get; private set; }
    [field: SerializeField] public TextMeshProUGUI MoneyText { get; private set; }

    void Start()
    {
        PlayerInventoryUi.Inventory = Player.PlayerInventory;
        PlayerFragmentUi.Inventory = Player.PlayerFragments;
    }

    void Update()
    {
        MoneyText.text = $"${Player.Money:0.00}";
    }
}
