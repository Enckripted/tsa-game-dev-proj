using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMachineButton : MonoBehaviour
{
    public IMachine Machine { get; set; }

    private TextMeshProUGUI buttonLabel;

    void Awake()
    {
        buttonLabel = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Machine.Running) Machine.AttemptMachineStop();
            else Machine.AttemptMachineStart();
        });
    }

    void Update()
    {
        if (Machine.Running) buttonLabel.text = "Stop";
        else buttonLabel.text = "Start";
    }
}
