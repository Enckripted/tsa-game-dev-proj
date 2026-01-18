using TMPro;
using UnityEngine;

public class FragmentQuantityUi : MonoBehaviour
{
    public FragmentQuantity Quantity;

    private TextMeshProUGUI textElement;

    void Awake()
    {
        textElement = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textElement.text = $"{Quantity.Type}: {Quantity.Amount}";
    }
}
