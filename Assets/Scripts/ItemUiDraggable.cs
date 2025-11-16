using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUiDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public TextMeshProUGUI nameText;

    public InventorySlot inventorySlot;
    public bool beingDragged { get; private set; }

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    void updateSprite()
    {
        if (inventorySlot.containsItem) nameText.text = inventorySlot.item.name;
        else nameText.text = "";
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        inventorySlot.changed.AddListener(updateSprite);
        updateSprite();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
        canvasGroup.blocksRaycasts = true;

        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        InventorySlot otherSlot = eventData.pointerDrag.GetComponent<ItemUiDraggable>().inventorySlot;
        Item currentItem = inventorySlot.pop();
        inventorySlot.insert(otherSlot.pop());
        otherSlot.insert(currentItem);
    }
}
