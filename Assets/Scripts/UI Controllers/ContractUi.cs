using UnityEngine;
using UnityEngine.UI;

public class ContractUi : MonoBehaviour
{
    [SerializeField] private GameObject contractListItemPrefab;
    [SerializeField] private Transform contractListContainer;

    [SerializeField] private ExpandedContractUi expandedUiPrefab;
    private ExpandedContractUi _expandedUiInstance;

    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Button closeButton;

    private ContractTileEntity _entity;
    private ItemUiDraggable _currentCratedSlot;

    // ...

    public void Init(ContractTileEntity entity)
    {
        _entity = entity;

        VerticalLayoutGroup layout = contractListContainer.GetComponent<VerticalLayoutGroup>();
        if (layout == null)
        {
            layout = contractListContainer.gameObject.AddComponent<VerticalLayoutGroup>();
        }

        layout.childAlignment = TextAnchor.UpperCenter;
        layout.childControlWidth = true;
        layout.childControlHeight = false;
        layout.childForceExpandHeight = false;
        layout.spacing = 35;
        layout.padding = new RectOffset(10, 10, 10, 10);

        UpdateUi();

        _entity.OnContractChanged.AddListener(UpdateUi);
        _entity.Inventory.Changed.AddListener(OnInventoryChanged);

        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }

    private void OnSubmit()
    {
        _entity.TryFulfillContract();
    }

    private void OnInventoryChanged()
    {
        if (_entity.AcceptedContract != null)
        {
            UpdateValidation();
        }
    }

    private void UpdateValidation()
    {
        if (_expandedUiInstance == null) return;

        IItem item = null;
        if (_entity.Inventory.SlotContainsItem(0))
        {
            item = _entity.Inventory.ItemInSlot(0);
        }

        string msg = _entity.AcceptedContract.GetValidationMessage(item);
        if (_expandedUiInstance.HelperText != null) _expandedUiInstance.HelperText.text = msg;

        bool isReady = msg == "Ready!";
        if (_expandedUiInstance.SubmitButton != null) _expandedUiInstance.SubmitButton.interactable = isReady;
    }

    private void UpdateUi()
    {
        bool hasAccepted = _entity.AcceptedContract != null;

        if (contractListContainer != null) contractListContainer.gameObject.SetActive(true);

        if (_expandedUiInstance == null && expandedUiPrefab != null)
        {
            _expandedUiInstance = Instantiate(expandedUiPrefab, contractListContainer);
            _expandedUiInstance.transform.localScale = Vector3.one;

            if (_expandedUiInstance.SubmitButton != null)
                _expandedUiInstance.SubmitButton.onClick.AddListener(OnSubmit);

            _expandedUiInstance.gameObject.SetActive(false);
        }

        if (hasAccepted)
        {
            foreach (Transform child in contractListContainer)
            {
                if (_expandedUiInstance != null && child == _expandedUiInstance.transform) continue;
                Destroy(child.gameObject);
            }

            if (_expandedUiInstance != null)
            {
                _expandedUiInstance.gameObject.SetActive(true);
                _expandedUiInstance.transform.SetAsLastSibling();

                if (_expandedUiInstance.DescriptionText != null)
                    _expandedUiInstance.DescriptionText.text = _entity.AcceptedContract.Description;

                if (_expandedUiInstance.RewardText != null)
                    _expandedUiInstance.RewardText.text = $"Reward: <color=yellow>${_entity.AcceptedContract.Reward:0.00}</color>";

                if (_currentCratedSlot == null && inventorySlotPrefab != null && _expandedUiInstance.InventorySlotContainer != null)
                {
                    foreach (Transform child in _expandedUiInstance.InventorySlotContainer) Destroy(child.gameObject);

                    GameObject slotObj = Instantiate(inventorySlotPrefab, _expandedUiInstance.InventorySlotContainer);

                    RectTransform rect = slotObj.GetComponent<RectTransform>();
                    if (rect != null)
                    {
                        rect.anchorMin = Vector2.zero;
                        rect.anchorMax = Vector2.one;
                        rect.offsetMin = Vector2.zero;
                        rect.offsetMax = Vector2.zero;
                        rect.localScale = Vector3.one;
                    }
                    else
                    {
                        slotObj.transform.localPosition = Vector3.zero;
                        slotObj.transform.localScale = Vector3.one;
                    }

                    _currentCratedSlot = slotObj.GetComponentInChildren<ItemUiDraggable>();
                    if (_currentCratedSlot != null)
                    {
                        _currentCratedSlot.InventorySlot = _entity.Inventory.GetSlot(0);
                        _currentCratedSlot.CanDropInSlot = true;
                    }
                }

                UpdateValidation();
            }
        }
        else
        {
            if (_expandedUiInstance != null)
            {
                _expandedUiInstance.gameObject.SetActive(false);

                if (_currentCratedSlot != null)
                {
                    if (_expandedUiInstance.InventorySlotContainer != null)
                    {
                        foreach (Transform child in _expandedUiInstance.InventorySlotContainer) Destroy(child.gameObject);
                    }
                    _currentCratedSlot = null;
                }
            }

            foreach (Transform child in contractListContainer)
            {
                if (_expandedUiInstance != null && child == _expandedUiInstance.transform) continue;
                Destroy(child.gameObject);
            }

            foreach (Contract contract in _entity.AvailableContracts)
            {
                GameObject itemObj = Instantiate(contractListItemPrefab, contractListContainer);

                itemObj.transform.localScale = Vector3.one;
                Vector3 pos = itemObj.transform.localPosition;
                pos.z = 0;
                itemObj.transform.localPosition = pos;

                ContractListItemUi itemUi = itemObj.GetComponent<ContractListItemUi>();
                if (itemUi != null)
                {
                    itemUi.Init(contract, _entity);
                }
            }
        }
    }

    private void Close()
    {
        if (TileEntityUiManager.Instance != null)
            TileEntityUiManager.Instance.CloseUi();
    }

    private void OnDestroy()
    {
        if (_entity != null)
        {
            _entity.OnContractChanged.RemoveListener(UpdateUi);
            _entity.Inventory.Changed.RemoveListener(OnInventoryChanged);
        }
    }
}
