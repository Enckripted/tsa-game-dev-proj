using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContractListItemUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button acceptButton;

    private Contract _contract;
    private ContractTileEntity _entity;

    public void Init(Contract contract, ContractTileEntity entity)
    {
        _contract = contract;
        _entity = entity;
        

        if (descriptionText != null)
        {
             descriptionText.text = contract.Description;
        }

        if (rewardText != null)
        {
             rewardText.text = $"Reward: <color=yellow>${contract.Reward:0.00}</color>";
        }

        if (acceptButton != null)
        {
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(OnAccept);
        }
    }

    private void OnAccept()
    {
        _entity.AcceptContract(_contract);
    }
}
