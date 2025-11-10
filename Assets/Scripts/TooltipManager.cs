using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tooltipText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip(Item referenceItem)
    {
        nameText.text = referenceItem.name;
        tooltipText.text = referenceItem.tooltip;

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
