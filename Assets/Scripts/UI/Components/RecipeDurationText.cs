using TMPro;
using UnityEngine;

public class RecipeDurationText : MonoBehaviour
{
    public IMachine Machine { get; set; }

    private TextMeshProUGUI label;

    void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Machine.Running) label.text = $"{Machine.SecondsRemaining:0.0}s";
        else label.text = "0.0s";
    }
}
