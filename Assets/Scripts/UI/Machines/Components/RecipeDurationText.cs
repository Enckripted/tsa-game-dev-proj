using TMPro;
using UnityEngine;

public class RecipeDurationText : MonoBehaviour
{
    private TextMeshProUGUI label;

    void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        IMachine machine = MachineUiManager.instance.currentMachine;
        if (machine.running) label.text = $"{machine.secondsRemaining:0.0}s";
        else label.text = "0.0s";
    }
}
