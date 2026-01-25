using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Lumin;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TooltipText;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;

    private Tooltip currentTooltip;
    private CanvasGroup canvasGroup;
    private new RectTransform transform;

    private DroppedItem DroppedItemUnderCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider == null || hit.collider.gameObject.GetComponent<DroppedItem>() == null) return null;
        return hit.collider.gameObject.GetComponent<DroppedItem>();
    }

    private ItemUiDraggable DraggableUnderCursor()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject != null && result.gameObject.GetComponent<ItemUiDraggable>())
                return result.gameObject.GetComponent<ItemUiDraggable>();
        }
        return null;
    }

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<RectTransform>();

        canvasGroup.alpha = 0;
    }

    void Update()
    {
        ItemUiDraggable itemDraggable = DraggableUnderCursor();
        DroppedItem droppedItem = DroppedItemUnderCursor();

        if (itemDraggable != null && itemDraggable.InventorySlot.ContainsItem)
            currentTooltip = itemDraggable.InventorySlot.StoredItem.HoverTooltip;
        else if (droppedItem != null)
            currentTooltip = droppedItem.Item.HoverTooltip;
        else
            currentTooltip = null;

        if (currentTooltip == null)
        {
            canvasGroup.alpha = 0;
            return;
        }

        Vector2 mousePos = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
        Vector2 pivot = new Vector2(0, 1);

        transform.position = mousePos;
        transform.pivot = pivot;

        //because adding rect.size.x to mousePos.x was reading inaccurately
        if (mousePos.x + transform.rect.size.x * transform.lossyScale.x > Screen.width) pivot.x = 1;
        if (mousePos.y - transform.rect.size.y * transform.lossyScale.y < 0) pivot.y = 0;
        transform.pivot = pivot;

        canvasGroup.alpha = 1;
        NameText.text = currentTooltip.NameText;
        TooltipText.text = currentTooltip.Text;
    }
}
