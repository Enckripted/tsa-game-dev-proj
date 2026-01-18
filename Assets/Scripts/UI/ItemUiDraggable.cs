using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUiDraggable : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI nameText;

    public InventorySlot InventorySlot;
    public bool CanDropInSlot = true;

    public bool BeingDragged { get; private set; }

    //there is no way to set z-index currently in unity, so we need to parent to a gameobject that is highest up in the hiearchy
    private GameObject dragPriorityObject;
    private GameObject slotObject;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private InputAction shiftAction;
    private Image image;

    void UpdateSprite()
    {
        if (InventorySlot.StoredItem == null)
        {
            image.enabled = false;
            return;
        }
        IItem item = InventorySlot.StoredItem;
        Sprite sprite;
        if (item.Type == ItemType.WandItem)
        {
            WandItem gearItem = item as WandItem;
            sprite = ItemSpriteService.GetItemSpriteFor(gearItem.BaseName, gearItem.WandMaterial);
        }
        else
        {
            throw new Exception("this code path shouldn't run unless we add new items!");
        }
        image.sprite = sprite;
        image.enabled = true;
    }

    bool CheckShiftClick()
    {
        if (shiftAction.IsPressed()) InventorySlot.QuickMove();
        return shiftAction.IsPressed();
    }

    void Awake()
    {
        dragPriorityObject = GameObject.Find("DragPriority");
        if (!dragPriorityObject) throw new Exception("No DragPriority object found in canvas for draggable objects");

        slotObject = transform.parent.gameObject;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        shiftAction = InputSystem.actions.FindAction("Sprint");
    }

    void Start()
    {
        InventorySlot.Changed.AddListener(UpdateSprite);
        UpdateSprite();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CheckShiftClick();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckShiftClick()) return;

        BeingDragged = true;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(dragPriorityObject.transform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    //onenddrag always fires before ondrop, so this position set always works
    public void OnEndDrag(PointerEventData eventData)
    {
        BeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(slotObject.transform, false);
        rectTransform.localPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(InventorySlot.CanInsert);
        if (eventData.pointerDrag == null || !InventorySlot.CanInsert) return;

        InventorySlot otherSlot = eventData.pointerDrag.GetComponent<ItemUiDraggable>().InventorySlot;
        IItem currentItem = InventorySlot.Pop();
        InventorySlot.Insert(otherSlot.Pop());
        otherSlot.Insert(currentItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InventorySlot.StoredItem != null) TooltipManager.Instance.ShowTooltip(InventorySlot.StoredItem.ItemTooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
