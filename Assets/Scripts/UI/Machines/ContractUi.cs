using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ContractUi : MonoBehaviour
{
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform inventorySlotContainer;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button closeButton;
    
    private ContractTileEntity _entity;

    public void Init(ContractTileEntity entity)
    {
        _entity = entity;
        
        GameObject slotObj = Instantiate(inventorySlotPrefab, inventorySlotContainer);
        slotObj.transform.localPosition = Vector3.zero;
        slotObj.transform.localScale = Vector3.one;

        ItemUiDraggable inventorySlot = slotObj.GetComponentInChildren<ItemUiDraggable>();
        
        if (inventorySlot != null)
        {
            inventorySlot.InventorySlot = entity.Inventory.GetSlot(0);
            inventorySlot.CanDropInSlot = true;
        }
        
        UpdateUi();
        
        _entity.OnContractChanged.AddListener(UpdateUi);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }
    
    private void UpdateUi()
    {
        if (_entity.CurrentContract != null)
        {
            if (descriptionText != null)
                descriptionText.text = _entity.CurrentContract.Description;
                
            if (rewardText != null)
            {
                string goldHex = GameColors.Instance != null ? GameColors.Instance.GoldColor.ToHexString() : "FFFF00";
                rewardText.text = $"Reward: <color=#{goldHex}>${_entity.CurrentContract.Reward:0.00}</color>";
            }
        }
        else
        {
            if (descriptionText != null) descriptionText.text = "No active contract.";
            if (rewardText != null) rewardText.text = "";
        }
    }

    private void Close()
    {
        if (TileEntityUiManager.instance != null)
            TileEntityUiManager.instance.closeUi();
    }

    private void OnDestroy()
    {
        if (_entity != null)
        {
            _entity.OnContractChanged.RemoveListener(UpdateUi);
        }
    }
}
