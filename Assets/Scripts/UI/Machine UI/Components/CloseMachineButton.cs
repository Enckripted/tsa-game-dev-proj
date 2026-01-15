using UnityEngine;
using UnityEngine.UI;

public class CloseMachineBtn : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            TileEntityUiManager.Instance.CloseUi();
        });
    }
}
