using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMachineButton : MonoBehaviour
{
    private TextMeshProUGUI buttonLabel;
    private IMachine machine;

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
        machine = NewMachineUiManager.instance.currentMachine;
        if (machine.running) buttonLabel.text = "Stop";
        else buttonLabel.text = "Start";
    }
}
