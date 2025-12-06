using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance { get; private set; }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tooltipText;

    private CanvasGroup canvasGroup;
    private new RectTransform transform;

    void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<RectTransform>();

        canvasGroup.alpha = 0;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 pivot = new Vector2(0, 1);

        transform.position = mousePos;
        transform.pivot = pivot;

        //because adding rect.size.x to mousePos.x was reading inaccurately
        if (mousePos.x + transform.rect.size.x * transform.lossyScale.x > Screen.width) pivot.x = 1;
        if (mousePos.y - transform.rect.size.y * transform.lossyScale.y < 0) pivot.y = 0;
        transform.pivot = pivot;
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
