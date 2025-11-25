using UnityEngine;
using UnityEngine.EventSystems;

public class MachineUiCloseButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        MachineUiManager.instance.closeUi();
    }
}
