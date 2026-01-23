using TMPro;
using UnityEngine;

public class PlayerToolbarUi : MonoBehaviour
{
    [field: SerializeField] public InventoryUi PlayerInventoryUi { get; private set; }
    [field: SerializeField] public FragmentInventoryUi PlayerFragmentUi { get; private set; }
    [field: SerializeField] public TextMeshProUGUI MoneyText { get; private set; }

    //very dirty hack and very bad
    void Awake()
    {
        PlayerInventoryUi.gameObject.SetActive(false);
        PlayerFragmentUi.gameObject.SetActive(false);
    }

    void Start()
    {
        PlayerInventoryUi.Inventory = Player.PlayerInventory;
        PlayerFragmentUi.Inventory = Player.PlayerFragments;
        PlayerInventoryUi.gameObject.SetActive(true);
        PlayerFragmentUi.gameObject.SetActive(true);
    }

    void Update()
    {
        MoneyText.text = $"${Player.Money:0.00}";
    }
}
