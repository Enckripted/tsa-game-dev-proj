using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpandedContractUi : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI DescriptionText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI RewardText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI HelperText { get; private set; }
    [field: SerializeField] public Button SubmitButton { get; private set; }
    [field: SerializeField] public Transform InventorySlotContainer { get; private set; }
}
