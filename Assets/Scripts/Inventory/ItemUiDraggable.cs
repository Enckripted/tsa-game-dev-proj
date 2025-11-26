using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUiDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private TextMeshProUGUI nameText;

    public InventorySlot inventorySlot;
    public bool canDropInSlot;

    public bool beingDragged { get; private set; }

    //there is no way to set z-index currently in unity, so we need to parent to a gameobject that is highest up in the hiearchy
    private GameObject dragPriorityObject;
    private GameObject slotObject;
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
        dragPriorityObject = GameObject.Find("DragPriority");
        if (!dragPriorityObject) throw new Exception("No DragPriority object found in canvas for draggable objects");
        slotObject = transform.parent.gameObject;
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
        transform.SetParent(dragPriorityObject.transform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.position = eventData.position;
    }

    //onenddrag always fires before ondrop, so this position set always works
    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(slotObject.transform, false);
        rectTransform.localPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !canDropInSlot) return;

        InventorySlot otherSlot = eventData.pointerDrag.GetComponent<ItemUiDraggable>().inventorySlot;
        Item currentItem = inventorySlot.pop();
        inventorySlot.insert(otherSlot.pop());
        otherSlot.insert(currentItem);
    }
}
