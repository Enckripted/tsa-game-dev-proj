using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUiDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventoryUiSlotNew uiSlot;
    [field: SerializeField] public bool beingDragged { get; private set; }

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag begin");
        beingDragged = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        beingDragged = false;
        canvasGroup.blocksRaycasts = true;

        //i don't think these getcomponent calls should be too expensive
        rectTransform.anchoredPosition = uiSlot.GetComponent<RectTransform>().anchoredPosition;
    }
}
