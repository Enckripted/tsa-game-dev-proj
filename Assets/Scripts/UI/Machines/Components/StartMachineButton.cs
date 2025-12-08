using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMachineButton : MonoBehaviour
{
    public IMachine machine { get; set; }

    private TextMeshProUGUI buttonLabel;

    void Awake()
    {
        buttonLabel = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (machine.running) machine.attemptMachineStop();
            else machine.attemptMachineStart();
        });
    }

    void Update()
    {
        if (machine.running) buttonLabel.text = "Stop";
        else buttonLabel.text = "Start";
    }
}
