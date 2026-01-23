using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TooltipText;

    private CanvasGroup canvasGroup;
    private new RectTransform transform;

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<RectTransform>();

        canvasGroup.alpha = 0;
    }

    void Update()
    {
        Vector2 mousePos = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
        Vector2 pivot = new Vector2(0, 1);

        transform.position = mousePos;
        transform.pivot = pivot;

        //because adding rect.size.x to mousePos.x was reading inaccurately
        if (mousePos.x + transform.rect.size.x * transform.lossyScale.x > Screen.width) pivot.x = 1;
        if (mousePos.y - transform.rect.size.y * transform.lossyScale.y < 0) pivot.y = 0;
        transform.pivot = pivot;
    }

    public void ShowTooltip(Tooltip tooltip)
    {
        NameText.text = tooltip.NameText;
        TooltipText.text = tooltip.Text;
        canvasGroup.alpha = 1;
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0;
    }
}
