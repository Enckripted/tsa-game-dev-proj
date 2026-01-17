using TMPro;
using UnityEngine;

public class FragmentQuantityUi : MonoBehaviour
{
    public string ComponentType { get; set; }

    private TextMeshProUGUI textElement;

    void Awake()
    {
        textElement = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textElement.text = $"{ComponentType}: {Player.PlayerComponents.GetQuantity(ComponentType)}";
    }
}
