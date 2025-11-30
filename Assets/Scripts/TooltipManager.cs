using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance { get; private set; }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tooltipText;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip(Item referenceItem)
    {
        nameText.text = referenceItem.name;
        tooltipText.text = referenceItem.tooltip;
        canvasGroup.alpha = 1;
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0;
    }
}
