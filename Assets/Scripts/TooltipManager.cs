using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance { get; private set; }

    public Transform tooltipTransform;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tooltipText;
    private GameObject tooltipGameObject;

    void Awake()
    {
        instance = this;

        tooltipGameObject = tooltipTransform.gameObject;
        tooltipGameObject.SetActive(false);
    }

    void Update()
    {
        tooltipTransform.position = Input.mousePosition;
    }

    public void ShowTooltip(Item referenceItem)
    {
        nameText.text = referenceItem.name;
        tooltipText.text = referenceItem.tooltip;

        tooltipGameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipGameObject.SetActive(false);
    }
}
